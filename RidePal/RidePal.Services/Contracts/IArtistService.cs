using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IArtistService
    {
        Task<ArtistDTO> GetArtistAsync(Guid id);
        PaginatedList<ArtistDTO> GetAllArtistsAsync(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
    }
}
