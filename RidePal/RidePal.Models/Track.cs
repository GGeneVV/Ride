using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Rank { get; set; }
        public int Duration { get; set; }
        public string Preview { get; set; }
        
        public virtual Artist Artist { get; set; }
        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }

    }
}
