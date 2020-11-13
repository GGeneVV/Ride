using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RidePal.Services.DTOModels
{
    public class UserDTO
    {
        public string UserName { get; set; }
        
        public virtual string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

        public string Token { get; set; }
        public ICollection<PlaylistDTO> Playlists { get; set; } = new List<PlaylistDTO>();

    }
}
