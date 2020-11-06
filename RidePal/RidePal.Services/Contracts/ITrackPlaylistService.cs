using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Services.Contracts
{
    public interface ITrackPlaylistService
    {
        TrackPlaylistDTO GetTrackPlaylistByIdAsync(Guid trackPlaylistId);

        ICollection<TrackPlaylistDTO> GetAllTrackPlaylistsAsync();
    }
}
