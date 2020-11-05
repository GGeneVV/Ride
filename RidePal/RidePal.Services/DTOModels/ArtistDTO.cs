using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Services.DTOModels
{
    public class ArtistDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<TrackDTO> Tracks { get; set; } = new List<TrackDTO>();
        public virtual ICollection<AlbumDTO> Albums { get; set; } = new List<AlbumDTO>();
    }
}
