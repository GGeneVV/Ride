using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Web.Models
{
    public class GenreVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Picture { get; set; }

        public ICollection<TrackVM> Tracks { get; set; } = new List<TrackVM>();
    }
}
