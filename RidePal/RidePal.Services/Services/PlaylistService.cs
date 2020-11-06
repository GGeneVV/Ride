using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services.Configurations;
using RidePal.Services.Contracts;
using RidePal.Services.Extensions;
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

        public PlaylistService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<Track>> TracksByRandom(string genre = "")
        {
            var tracks = await _appDbContext.Tracks
                .AsNoTracking()
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Genre)
                .WhereIf(!string.IsNullOrEmpty(genre), t => t.Genre.Name == genre)
                .OrderBy(t => Guid.NewGuid())
                .AsQueryable()
                //.Select(t => _mapper.Map<TrackDTO>(t))
                .ToListAsync();

            return tracks;
        }

        public async Task<List<Track>> TracksByRandom(PlaylistConfig playlistConfig)
        {
            var genreNames = playlistConfig.GenreConfigs
                .Where(g => g.IsChecked == true)
                .Select(g => g.Name);

            var tracks = await _appDbContext.Tracks
                .AsNoTracking()
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Genre)
                .Where(t => genreNames.Contains(t.Genre.Name))
                    //.Where(x => x.IsChecked == true)
                    //.Select(x => x.Name)
                    //.ToList()
                    //.Contains(t.Genre.Name)
                //.Where(t => playlistConfig.GenreConfigs
                //    .Where(x => x.IsChecked == true)
                //    .Select(x => x.Name)
                //    .ToList().Any(s => t.Genre.Name.Contains(s))
                //)
                .OrderBy(t => Guid.NewGuid())
                //.Select(t => _mapper.Map<TrackDTO>(t))
                .ToListAsync();

            return tracks;
        }

        public async Task<List<Track>> TopTracks(string genre)
        {
            var tracks = await _appDbContext.Tracks
                .AsNoTracking()
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Genre)
                .WhereIf(!string.IsNullOrEmpty(genre), t => t.Genre.Name == genre)
                .OrderBy(t => t.Rank)
                .AsQueryable()
                //.Select(t => _mapper.Map<TrackDTO>(t))
                .ToListAsync();

            return tracks;
        }

        //TODO: can make GenreDTO with isChecked & Percentage LIST<GENREDTO> isCHECKED = true; PERCENTE = 100;
        public async Task<Playlist> GeneratePlaylist(int travelDuration, bool pop = false, bool rap = false, bool rock = false, bool dance = false, bool rnb = false)
        {
            List<Track> tracks = new List<Track>();
            int totalDuration = 0;
            int genresCount = new List<bool>() { pop, rap, rock, dance, rnb }.Count(x => x == true);
            var avgMinPerGenre = 300 / genresCount;

            var playlist = new Playlist()
            {
                CreatedOn = DateTime.Now,
                Title = "MyPlaylist",

            };

            //TODO: do this for all genres 
            int durationPerGenre = travelDuration * ( (100 / genresCount) / 100 );

            var orderedTracks = await TracksByRandom("Pop");

            int count = 0;
            while (true)
            {
                if (count >= orderedTracks.Count)
                {
                    break;
                }
                var track = orderedTracks[count];

                if (totalDuration <= durationPerGenre + avgMinPerGenre 
                    && totalDuration >= travelDuration)//&& totalDuration <= travelDuration + 5)
                {
                    break;
                }
                tracks.Add(track);
                totalDuration += track.Duration;

                playlist.TrackPlaylists.Add(new TrackPlaylist()
                {
                    CreatedOn = DateTime.Now,
                    Playlist = playlist,
                    Track = track,
                });
                count++;
            }
            playlist.Duration = totalDuration;
            return playlist;
        }

        public async Task<Playlist> GeneratePlaylist(int travelDuration, PlaylistConfig playlistConfig)
        {
            List<Track> tracks = new List<Track>();
            int totalDuration = 0;
            var genres = playlistConfig.GenreConfigs.Where(x => x.IsChecked == true);
            int genresCount = genres.Count();
            var avgMinPerGenre = 300 / genresCount;



            var playlist = new Playlist()
            {
                CreatedOn = DateTime.Now,
                Title = "MyPlaylist",

            };

            var orderedTracks = await TracksByRandom(playlistConfig);

            foreach (var genre in genres)
            {
                int durationPerGenre = 0;
                int genreDuration = 0;

                var genreTracks = orderedTracks
                    .Where(x => x.Genre.Name == genre.Name)
                    .ToList();

                if (genre.Percentage <= 0)
                {
                    double genrePercent = 100 / genresCount;
                    durationPerGenre = (int)Math.Floor(travelDuration * genrePercent) / 100;
                }
                else
                {
                    durationPerGenre = travelDuration * (genre.Percentage / 100);
                }

                int count = 0;
                while (true)
                {
                    if (count >= genreTracks.Count)
                    {
                        break;
                    }
                    var track = genreTracks[count];

                    if ((totalDuration <= durationPerGenre + avgMinPerGenre
                        && totalDuration >= travelDuration)
                        || totalDuration == travelDuration)//&& totalDuration <= travelDuration + 5)
                    {
                        break;
                    }

                    if (genreDuration <= durationPerGenre + avgMinPerGenre 
                        && genreDuration >= durationPerGenre
                        || genreDuration >= durationPerGenre + avgMinPerGenre)
                    {
                        break;
                    }
                    tracks.Add(track);
                    totalDuration += track.Duration;
                    genreDuration += track.Duration;
                    playlist.TrackPlaylists.Add(new TrackPlaylist()
                    {
                        CreatedOn = DateTime.Now,
                        Playlist = playlist,
                        Track = track,
                    });
                    count++;
                }

            }


            playlist.Duration = totalDuration;
            return playlist;
        }

    }


}
