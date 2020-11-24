using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IAlbumService
    {
        Task<AlbumDTO> GetAlbumByIdAsync(Guid albumId);

        IQueryable<AlbumDTO> GetAllAlbumsAsync(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");

        IReadOnlyCollection<AlbumDTO> GetTopAlbums(int count = 5, string searchString = "");

    }
}
