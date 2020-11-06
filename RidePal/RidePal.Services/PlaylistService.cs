using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Models;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<Track>> TracksByRandom(string genre)
        {
            var tracks = await _appDbContext.Tracks
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Genre)
                .Where(t => t.Genre.Name == genre)
                .OrderBy(t => Guid.NewGuid())
                //.Select(t => _mapper.Map<TrackDTO>(t))
                .ToListAsync();

            return tracks;
        }

        public async Task<List<Track>> TopTracks(string genre)
        {
            var tracks = await _appDbContext.Tracks
                .Where(t => t.IsDeleted == false)
                .Include(t => t.Genre)
                .Where(t => t.Genre.Name == genre)
                .OrderBy(t => t.Rank)
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
    }
}
