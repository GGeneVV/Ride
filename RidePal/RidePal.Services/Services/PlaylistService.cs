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
using System.Threading;
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

        public IQueryable<Track> RandomTracksByConfig(PlaylistConfig playlistConfig)
        {
            var genreNames = playlistConfig.GenreConfigs
                .Where(g => g.IsChecked == true)
                .Select(g => g.Name);

            var tracks = _appDbContext.Tracks
                .AsNoTracking()
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Genre)
                .Where(t => genreNames.Contains(t.Genre.Name))
                .OrderBy(t => Guid.NewGuid());

            return tracks;
        }

        public async Task<Playlist> GeneratePlaylist(int travelDuration, PlaylistConfig playlistConfig)
        {
            int totalDuration = 0;

            var genres = playlistConfig.GenreConfigs
                .Where(x => x.IsChecked == true); // Get only checked genres

            int genresCount = genres.Count();
            int avgMinPerGenre = 300 / genresCount; // Get Average additional minute per genre ( 5min MAX )


            //Initialize playlist
            var playlist = new Playlist()
            {
                CreatedOn = DateTime.Now,
                Title = "MyPlaylist",
            };

            List<TrackPlaylist> trackPlaylist = new List<TrackPlaylist>();
            IQueryable<Track> orderedTracks = RandomTracksByConfig(playlistConfig);

            foreach (var genre in genres)
            {
                int durationPerGenre = 0;
                int genreDuration = 0;

                var genreTracks = await orderedTracks
                    .Where(x => x.Genre.Name == genre.Name)
                    .ToListAsync();

                if (genre.Percentage <= 0 && playlistConfig.IsAdvanced == false)
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
                    if (count >= genreTracks.Count) { break; }

                    var track = genreTracks[count];

                    //Break if next track will surpass Max duation per genre
                    if (genreDuration + track.Duration > durationPerGenre + avgMinPerGenre) 
                        break;

                    totalDuration += track.Duration;
                    genreDuration += track.Duration;
                    trackPlaylist.Add(new TrackPlaylist()
                    {
                        CreatedOn = DateTime.Now,
                        Playlist = playlist,
                        Track = track,
                    });
                    count++;
                }
            }
            if (playlistConfig.UseTopTracks)
            {
                trackPlaylist = trackPlaylist.OrderBy(t => t.Track.Rank).ToList();
            }
            playlist.Duration = totalDuration;
            playlist.TrackPlaylists = trackPlaylist;
            return playlist;
        }
    }
}
