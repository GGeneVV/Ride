using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Services.Configurations
{
    public class PlaylistConfig
    {
        public bool UseTopTracks { get; set; }
        public bool AllowTracksFromSameArtist { get; set; }
        public ICollection<PlaylistGenreConfig> GenreConfigs { get; set; } = new List<PlaylistGenreConfig>();
    }
}
