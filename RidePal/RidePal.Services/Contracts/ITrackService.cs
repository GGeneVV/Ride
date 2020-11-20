using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface ITrackService
    {
        Task<TrackDTO> GetTrackAsync(Guid id);
        IQueryable<TrackDTO> GetAllTracks(
            int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");

        Task<IReadOnlyCollection<TrackDTO>> GetPopularTracksAsync(int count = 5);
        IReadOnlyCollection<TrackDTO> GetTopTracks(int count = 5, string searchString = "");
    }
}
