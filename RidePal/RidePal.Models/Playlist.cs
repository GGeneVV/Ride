using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RidePal.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public bool Enabled { get; set; }
        public bool IsArtistRepeated { get; set; }
        public bool IsTopTracksEnabled { get; set; }

        public virtual UserDetail Creator { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public string Picture { get; set; }
    }
}
