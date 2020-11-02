using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
