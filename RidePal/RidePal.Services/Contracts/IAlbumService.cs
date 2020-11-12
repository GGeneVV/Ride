using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;

namespace RidePal.Services.Contracts
{
    public interface IAlbumService
    {
        AlbumDTO GetAlbumByIdAsync(Guid albumId);

        PaginatedList<AlbumDTO> GetAllAlbumsAsync(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");

    }
}
