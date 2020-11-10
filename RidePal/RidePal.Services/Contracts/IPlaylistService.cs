using RidePal.Models;
using RidePal.Services.Configurations;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IPlaylistService
    {
        Task<IReadOnlyCollection<TrackDTO>> RandomTracksByGenreConfig(PlaylistConfig playlistConfig, string genreName);
        Task<PlaylistDTO> GeneratePlaylist(int travelDuration, PlaylistConfig playlistConfig);
        IQueryable<PlaylistDTO> GetUserPlaylists(User user);
        Task<PlaylistDTO> GetPlaylist(Guid? id);
        IQueryable<PlaylistDTO> GetAllPlaylists();
        Task DeletePlaylist(Guid? id);
        Task<PlaylistDTO> EditPlaylist(Guid? id, PlaylistDTO updatedPlaylist);

    }
}
