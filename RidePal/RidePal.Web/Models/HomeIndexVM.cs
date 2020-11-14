using System.Collections.Generic;

namespace RidePal.Web.Models
{
    public class HomeIndexVM
    {
        //For Generating playlist
        public PlaylistConfigVM PlaylistConfig { get; set; }


        public IReadOnlyCollection<TrackVM> PopularTracks { get; set; } = new List<TrackVM>();
        public IReadOnlyCollection<TrackVM> TopTracks { get; set; } = new List<TrackVM>();
        public IReadOnlyCollection<ArtistVM> TopArtists { get; set; } = new List<ArtistVM>();
        public IReadOnlyCollection<AlbumVM> TopAlbums { get; set; } = new List<AlbumVM>();
    }
}
