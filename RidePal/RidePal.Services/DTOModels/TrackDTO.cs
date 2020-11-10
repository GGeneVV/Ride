using RidePal.Models;
using System;

namespace RidePal.Services.DTOModels
{
    public class TrackDTO
    {
        public Guid Id { get; set; }
        public string DeezerId { get; set; }
        public string Title { get; set; }
        public string SongURL { get; set; }
        public int Duration { get; set; }
        public int Rank { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Preview { get; set; }
        public virtual AlbumDTO Album { get; set; }
        public Guid? AlbumId { get; set; }
        public virtual ArtistDTO Artist { get; set; }
        public Guid? ArtistId { get; set; }
        public virtual GenreDTO Genre { get; set; }
        public Guid? GenreId { get; set; }

    }
}
