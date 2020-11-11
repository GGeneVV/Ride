using RidePal.Models;
using RidePal.Services.DTOModels;
using RidePal.Services.DTOModels.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IPlaylistService
    {
        Task<IReadOnlyCollection<TrackDTO>> RandomTracksByGenreConfig(PlaylistConfigDTO playlistConfigDTO, string genreName);
        Task<PlaylistDTO> GeneratePlaylist(int travelDuration, PlaylistConfigDTO playlistConfigDTO);
        IQueryable<PlaylistDTO> GetUserPlaylists(User user);
        Task<PlaylistDTO> GetPlaylist(Guid? id);
        IQueryable<PlaylistDTO> GetAllPlaylists();
        Task DeletePlaylist(Guid? id);
        Task<PlaylistDTO> EditPlaylist(Guid? id, PlaylistDTO updatedPlaylist);

    }
}
