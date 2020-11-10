using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RidePal.Services.Configurations
{
    public class PlaylistConfig
    {
        public bool UseTopTracks { get; set; }
        public bool AllowTracksFromSameArtist { get; set; }
        public bool IsAdvanced { get; set; }

        //[BindProperty]
        public IList<PlaylistGenreConfig> GenreConfigs { get; set; } = new List<PlaylistGenreConfig>();
    }
}
