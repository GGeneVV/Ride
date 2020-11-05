using RidePal.Models;
using System;

namespace RidePal.Services.DTOModels
{

    public class AlbumDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        public string Tracklist { get; set; }
        public virtual Artist Artist { get; set; }
        public Guid? ArtistId { get; set; }


    }


}
