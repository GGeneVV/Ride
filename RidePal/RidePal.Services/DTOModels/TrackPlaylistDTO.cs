using RidePal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Services.DTOModels
{
    //TODO: we don't need this
    public class TrackPlaylistDTO
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
    }
}
