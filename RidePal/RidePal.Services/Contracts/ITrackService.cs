using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface ITrackService
    {
        Task<TrackDTO> GetTrackAsync(Guid id);
        PaginatedList<TrackDTO> GetAllTracks(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");

        Task<IReadOnlyCollection<TrackDTO>> GetPopularTracksAsync(int count = 5);
        IReadOnlyCollection<TrackDTO> GetTopTracks(int count = 5, string searchString = "");
    }
}
