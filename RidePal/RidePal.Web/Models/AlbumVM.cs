using System;

namespace RidePal.Web.Models
{
    public class AlbumVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Tracklist { get; set; }
        public virtual ArtistVM Artist { get; set; }
        public Guid? ArtistId { get; set; }
    }
}
