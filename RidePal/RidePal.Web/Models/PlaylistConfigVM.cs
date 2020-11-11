using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Web.Models
{
    public class PlaylistConfigVM
    {
        public bool UseTopTracks { get; set; }
        public bool AllowTracksFromSameArtist { get; set; }
        public bool IsAdvanced { get; set; }

        //[BindProperty]
        public IList<GenreVM> GenreConfigs { get; set; } 
    }
}
