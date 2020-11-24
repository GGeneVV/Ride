using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RidePal.Services.Contracts
{
    public interface IAlbumService
    {
        AlbumDTO GetAlbumByIdAsync(Guid albumId);

        IQueryable<AlbumDTO> GetAllAlbums(
            int? pageNumber = 1,
            string sortOrder = "",
            string searchString = "");

        IQueryable<AlbumDTO> GetTopAlbums(int count = 5, string searchString = "");

    }
}
