using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RidePal.Services.DTOModels.Configurations
{
    public class PlaylistConfigDTO
    {
        public bool UseTopTracks { get; set; }
        public bool AllowTracksFromSameArtist { get; set; }
        public bool IsAdvanced { get; set; }

        //[BindProperty]
        public IList<PlaylistGenreConfigDTO> GenreConfigs { get; set; }
    }
}
