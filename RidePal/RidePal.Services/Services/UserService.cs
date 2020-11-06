using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RidePal.Services
{
    public class UserService : IUserService
    {
        public Task<PlaylistDTO> EditPlaylist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistDTO> GetPlaylist(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PlaylistDTO>> GetPlaylists()
        {
            throw new NotImplementedException();
        }

        public Task<PlaylistDTO> RemovePlaylist(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
