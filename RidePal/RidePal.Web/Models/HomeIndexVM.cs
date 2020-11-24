using System.Collections.Generic;
using System.Linq;

namespace RidePal.Web.Models
{
    public class HomeIndexVM
    {
        //For Generating playlist
        public PlaylistConfigVM PlaylistConfig { get; set; }


        public IEnumerable<TrackVM> PopularTracks { get; set; }// = new List<TrackVM>();
        public IEnumerable<TrackVM> TopTracks { get; set; }// = new List<TrackVM>();
        public IEnumerable<ArtistVM> TopArtists { get; set; }// = new List<ArtistVM>();
        public IEnumerable<AlbumVM> TopAlbums { get; set; }// = new List<AlbumVM>();
    }
}
