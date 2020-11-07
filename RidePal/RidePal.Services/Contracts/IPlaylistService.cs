using RidePal.Models;
using RidePal.Services.Configurations;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IPlaylistService
    {
        IQueryable<Track> TracksByConfig(PlaylistConfig playlistConfig);
        Task<Playlist> GeneratePlaylist(int travelDuration, PlaylistConfig playlistConfig);
    }
}
