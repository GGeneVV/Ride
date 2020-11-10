using System;
using System.Collections.Generic;

namespace RidePal.Web.Models
{
    public class ArtistVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<TrackVM> Tracks { get; set; } = new List<TrackVM>();
        public virtual ICollection<AlbumVM> Albums { get; set; } = new List<AlbumVM>();
    }
}
