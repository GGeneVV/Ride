﻿using RidePal.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Models
{
    public class TrackPlaylist : Entity
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; }

        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; }

    }
}