using RidePal.Models;
using RidePal.Services.Configurations;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IPlaylistService
    {
        Task<List<Track>> TracksByRandom(string genre);
        Task<List<Track>> TopTracks(string genre);
        Task<Playlist> GeneratePlaylist(int travelDuration, bool pop = false, bool rap = false, bool rock = false, bool dance = false, bool rnb = false);
        Task<Playlist> GeneratePlaylist(int travelDuration, PlaylistConfig playlistConfig);
    }
}
