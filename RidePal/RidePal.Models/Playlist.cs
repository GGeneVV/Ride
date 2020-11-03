using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RidePal.Models
{
    public class Playlist
    {
        [Required]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public int Duration { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int Rank { get; set; }
        public bool Enabled { get; set; }
        public bool IsArtistRepeated { get; set; }
        public bool IsTopTracksEnabled { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public Guid TrackId { get; set; }

        public Track Track { get; set; }

       // public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    }
}
