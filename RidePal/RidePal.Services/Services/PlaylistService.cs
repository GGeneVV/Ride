using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace RidePal.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PlaylistService(AppDbContext appDbContext, IMapper mapper, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<TrackDTO>> RandomTracksByGenreConfig(PlaylistConfigDTO playlistConfigDTO, string genreName)
        {
            var genreNames = playlistConfigDTO.GenreConfigs
                .Where(g => g.IsChecked == true)
                .Select(g => g.Name);

            var tracks = await _appDbContext.Tracks
                .AsNoTracking()
                .Where(t => t.IsDeleted == false)
                .Include(a => a.Album)
                .Include(a => a.Artist)
                .Include(g => g.Genre)
                .Where(t => genreNames.Contains(t.Genre.Name) && t.Genre.Name.Equals(genreName))
                .Select(t => _mapper.Map<TrackDTO>(t))
                .OrderBy(t => Guid.NewGuid())
                .ToListAsync();

            return tracks;
        }

        public async Task<PlaylistDTO> GeneratePlaylist(int travelDuration, PlaylistConfigDTO playlistConfigDTO)
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
                Title = "MyPlaylist",
            };

            List<TrackPlaylist> trackPlaylist = new List<TrackPlaylist>();

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
                    //var track = genreTracks
                    //    .Skip(count)
                    //    .FirstOrDefault();

                    //Break if next track will surpass Max duation per genre
                    if (genreDuration + track.Duration > durationPerGenre + avgMinPerGenre) { break; }

                    totalDuration += track.Duration;
                    genreDuration += track.Duration;
                    trackPlaylist.Add(new TrackPlaylist()
                    {
                        CreatedOn = DateTime.Now,
                        Playlist = playlist,
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
            playlist.Duration = totalDuration;
            playlist.TrackPlaylists = trackPlaylist;
            var playlistDTO = _mapper.Map<PlaylistDTO>(playlist);

            return playlistDTO;
        }

        public IQueryable<PlaylistDTO> GetUserPlaylists(User user)
        {
            if (user == null) { throw new ArgumentNullException(); }

            var playlist = _appDbContext.Playlists
                .Where(p => p.UserId == user.Id && p.IsDeleted == false)
                .Select(p => _mapper.Map<PlaylistDTO>(p));

            return playlist;
        }

        public async Task<PlaylistDTO> GetPlaylist(Guid? id)
        {
            if (id == null) { throw new ArgumentNullException(); }

            var playlist = await _appDbContext.Playlists
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null) { throw new ArgumentNullException(); }

            var dto = _mapper.Map<PlaylistDTO>(playlist);

            return dto;
        }

        public IQueryable<PlaylistDTO> GetAllPlaylists()
        {
            var playlist = _appDbContext.Playlists
                .Where(p => p.IsDeleted == false)
                .Select(p => _mapper.Map<PlaylistDTO>(p));

            return playlist;
        }

        public async Task DeletePlaylist(Guid? id)
        {
            if (id == null) { throw new ArgumentNullException(); }

            var playlist = await _appDbContext.Playlists
                .Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist == null) { throw new ArgumentNullException(); }

            playlist.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<PlaylistDTO> EditPlaylist(Guid? id, PlaylistDTO updatedPlaylist)
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
    }
}
