using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Web.Models
{
    public class GeneratePlaylistAPIVM
    {
        public int TravelDuration { get; set; }
        public string Title { get; set; }
        public bool UseTopTracks { get; set; }
        public bool AllowTracksFromSameArtist { get; set; }
        public bool IsAdvanced { get; set; }
        public bool IsArtistRepeated { get; set; }
        public bool IsTopTracksEnabled { get; set; }

        //[BindProperty]
        public IList<GenreConfigVM> GenreConfigs { get; set; } = new List<GenreConfigVM>();
        public Guid userId { get; set; }
    }
}
