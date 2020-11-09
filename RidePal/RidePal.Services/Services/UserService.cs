using AutoMapper;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;


        public UserService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public Task<PlaylistDTO> EditPlaylist(Guid userId, Guid playlistId)
        {
            try
            {
                var playlist = _db.Playlists.Where(p => p.UserId == userId).FirstOrDefault(u => u.Id == playlistId);
                var dto = _mapper.Map<PlaylistService>(playlist);
                //changing the title or associated genre??
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
