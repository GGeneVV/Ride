using System;
using System.Collections.Generic;
using System.Text;

namespace RidePal.Services.DTOModels
{
    public class EditPlaylistDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool Revive { get; set; }
    }
}
