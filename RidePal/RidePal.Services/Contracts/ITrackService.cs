using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface ITrackService
    {
        Task<TrackDTO> GetTrackAsync(Guid id);
        PaginatedList<TrackDTO> GetAllTracksAsync(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
    }
}
