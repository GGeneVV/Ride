using RidePal.Services.DTOModels;
using RidePal.Services.DTOModels.Configurations;
using RidePal.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IPlaylistService
    {
        Task<IReadOnlyCollection<TrackDTO>> RandomTracksByGenreConfig(PlaylistConfigDTO playlistConfigDTO, string genreName);
        Task<PlaylistDTO> GeneratePlaylist(int travelDuration, PlaylistConfigDTO playlistConfigDTO);
        PaginatedList<PlaylistDTO> GetUserPlaylists(
            UserDTO userDTO,
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
        Task<PlaylistDTO> GetPlaylist(Guid? id);
        PaginatedList<PlaylistDTO> GetAllPlaylists(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
        Task DeletePlaylist(Guid? id);
        Task<PlaylistDTO> EditPlaylist(Guid? id, PlaylistDTO updatedPlaylist);

    }
}
