using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Collections.Generic;

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

        IReadOnlyCollection<AlbumDTO> GetTopAlbums(int count = 5, string searchString = "");

    }
}
