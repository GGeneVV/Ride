using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface ITrackService
    {
        Task<TrackDTO> GetTrackAsync(Guid id);
        Task<IEnumerable<TrackDTO>> GetAllTracksAsync(int? page = 1, int? pagesize = 10);
    }
}
