using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Services.Extensions;
using RidePal.Services.Pagination;
using RidePal.Services.Wrappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RidePal.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserManagerWrapper _userManagerWrapper;

        public PlaylistService(AppDbContext appDbContext, IMapper mapper,
            IUserManagerWrapper userManagerWrapper,
            IUserService userService
            )
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userManagerWrapper = userManagerWrapper;
            _userService = userService;
            
        }

        public async Task<IReadOnlyCollection<TrackDTO>> RandomTracksByGenreConfig(PlaylistConfigDTO playlistConfigDTO, string genreName)
        {
            var genreNames = playlistConfigDTO.GenreConfigs
                .Where(g => g.IsChecked == true)
                .Select(g => g.Name);

            var tracks = await _appDbContext.Tracks
                .Where(t => t.IsDeleted == false)
                .Include(g => g.Genre)
                .AsNoTracking()
                .Where(t => genreNames.Contains(t.Genre.Name) && t.Genre.Name.Equals(genreName))
                .Select(t => _mapper.Map<TrackDTO>(t))
                .OrderBy(t => Guid.NewGuid())
                .ToListAsync();

            return tracks;
        }

        public async Task<PlaylistDTO> GeneratePlaylist(int travelDuration, PlaylistConfigDTO playlistConfigDTO,Guid userId)
        {

            if (travelDuration <= 0 || playlistConfigDTO == null) { throw new ArgumentNullException(); }
            int totalDuration = 0;

            var genres = playlistConfigDTO.GenreConfigs
                .Where(x => x.IsChecked == true); // Get only checked genres

            int genresCount = genres.Count();
            if (genresCount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            int avgMinPerGenre = 300 / genresCount; // Get Average additional minute per genre ( 5min MAX )

            //Initialize playlist
            var playlist = new Playlist()
            {
                CreatedOn = DateTime.Now,
                Title = playlistConfigDTO.Title,
                UserId = userId,
            };

            ICollection<TrackPlaylist> trackPlaylist = new List<TrackPlaylist>();

            foreach (var genre in genres)
            {
                int durationPerGenre = 0;
                int genreDuration = 0;
                var genreTracks = await RandomTracksByGenreConfig(playlistConfigDTO, genre.Name);

                if (genre.Percentage <= 0 && playlistConfigDTO.IsAdvanced == false)
                {
                    double genrePercent = 100 / genresCount;
                    durationPerGenre = (int)Math.Floor(travelDuration * genrePercent) / 100;
                }
                else
                {
                    durationPerGenre = (travelDuration * genre.Percentage) / 100;
                }

                int count = 0;
                while (true)
                {
                    if (count >= genreTracks.Count()) { break; }

                    //Gets Element at index 
                    var track = genreTracks.ElementAt(count);

                    //Break if next track will surpass Max duation per genre
                    if (genreDuration + track.Duration > durationPerGenre + avgMinPerGenre) { break; }

                    totalDuration += track.Duration;
                    genreDuration += track.Duration;
                    trackPlaylist.Add(new TrackPlaylist()
                    {
                        CreatedOn = DateTime.Now,
                        PlaylistId = playlist.Id,
                        Playlist = playlist,
                        TrackId = track.Id,
                        Track = _mapper.Map<Track>(track),
                    });

                    count++;
                }
            }
            if (playlistConfigDTO.UseTopTracks)
            {
                trackPlaylist = trackPlaylist.OrderBy(t => t.Track.Rank).ToList();
            }
            else
            {
                trackPlaylist = trackPlaylist.OrderByDescending(t => t.Track.Rank).ToList();
            }

            playlist.Rank = (int)trackPlaylist.Average(t => t.Track.Rank);
            playlist.Duration = totalDuration;
            var trackPlaylistDB = trackPlaylist.Select(x => new TrackPlaylist() { PlaylistId = x.PlaylistId, TrackId = x.TrackId, }).ToList();
            playlist.TrackPlaylists = trackPlaylistDB;
            await _appDbContext.Playlists.AddAsync(playlist);
            await _appDbContext.SaveChangesAsync();
            playlist.TrackPlaylists = trackPlaylist;

            var playlistDTO = _mapper.Map<PlaylistDTO>(playlist);

            return playlistDTO;
        }

        public PaginatedList<PlaylistDTO> GetUserPlaylists(
            Guid userId,
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            if (userId == null) { throw new ArgumentNullException(); }
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            currentFilter = searchString;

            var playlists = _appDbContext.Playlists
                .Include(x => x.TrackPlaylists)
                .Where(p => p.UserId == userId && p.IsDeleted == false)
                .WhereIf(!String.IsNullOrEmpty(searchString), s => s.Title.Contains(searchString))
                .Select(p => _mapper.Map<PlaylistDTO>(p));

            switch (sortOrder)
            {
                case "rank_desc":
                    playlists = playlists.OrderBy(b => b.Rank);
                    break;
                case "Duration":
                    playlists = playlists.OrderBy(b => b.Duration);
                    break;
                case "Duration_decs":
                    playlists = playlists.OrderByDescending(s => s.Duration);
                    break;
                default:
                    playlists = playlists.OrderByDescending(s => s.Rank);
                    break;
            }

            int pageSize = 10;
           
            return PaginatedList<PlaylistDTO>.Create(playlists.AsQueryable(), pageNumber ?? 1, pageSize);
        }

        public async Task<PlaylistDTO> GetPlaylist(Guid id)
        {
            if (id == null) { throw new ArgumentNullException(); }

            var playlist =await _appDbContext.Playlists
                .Include(x => x.TrackPlaylists)
                .Where(p => p.IsDeleted == false && p.Id == id)
                .FirstOrDefaultAsync();
            
            if (playlist == null) { throw new ArgumentNullException(); }

            var dto = _mapper.Map<PlaylistDTO>(playlist);

            return dto;
        }

        public PaginatedList<PlaylistDTO> GetAllPlaylists(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            currentFilter = searchString;

            var playlists = _appDbContext.Playlists
                .Include(x => x.TrackPlaylists)
                .Where(p => p.IsDeleted == false)
                .WhereIf(!String.IsNullOrEmpty(searchString), s => s.Title.Contains(searchString))
                .Select(p => _mapper.Map<PlaylistDTO>(p));

            switch (sortOrder)
            {
                case "rank_desc":
                    playlists = playlists.OrderByDescending(b => b.Rank);
                    break;
                case "Duration":
                    playlists = playlists.OrderBy(b => b.Duration);
                    break;
                case "Duration_decs":
                    playlists = playlists.OrderByDescending(s => s.Duration);
                    break;
                default:
                    playlists = playlists.OrderBy(s => s.Rank);
                    break;
            }

            int pageSize = 10;
           
            return PaginatedList<PlaylistDTO>.Create(playlists.AsQueryable(), pageNumber ?? 1, pageSize);

        }

        public async Task DeletePlaylist(Guid id)
        {
            if (id == null) { throw new ArgumentNullException(); }

            var playlist = await _appDbContext.Playlists
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null) { throw new ArgumentNullException(); }

            playlist.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<PlaylistDTO> EditPlaylist(Guid id, PlaylistDTO updatedPlaylist)
        {
            if (id == null || updatedPlaylist == null) { throw new ArgumentNullException(); }

            var playlist = await _appDbContext.Playlists
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null) { throw new ArgumentNullException(); }

            playlist.Title = updatedPlaylist.Title;
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<PlaylistDTO>(playlist);
        }

        public async Task<PlaylistDTO> SavePlaylist(PlaylistDTO playlistDTO)
        {
            if (playlistDTO == null) { throw new ArgumentNullException(); }

            var playlist = _mapper.Map<Playlist>(playlistDTO);
            var trackPlaylist = playlist.TrackPlaylists;
            await _appDbContext.TrackPlaylists.AddRangeAsync(trackPlaylist);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<PlaylistDTO>(playlist);
        }
    }
}
