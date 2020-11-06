using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RidePal.Services.Contracts
{
    public interface IUserService
    {
        Task<PlaylistDTO> GetPlaylist(Guid id);
        Task<ICollection<PlaylistDTO>> GetPlaylists();
        Task<PlaylistDTO> EditPlaylist(Guid id);
        Task<PlaylistDTO> RemovePlaylist(Guid id);
    }
}
