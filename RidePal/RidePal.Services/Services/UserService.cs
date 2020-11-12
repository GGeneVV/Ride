using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RidePal.Data;
using RidePal.Services.Contracts;
using RidePal.Services.DTOModels;
using RidePal.Services.Extensions;
using RidePal.Services.Pagination;
using RidePal.Services.Wrappers.Contracts;
using System;
using System.Linq;

namespace RidePal.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IUserManagerWrapper _userManagerWrapper;


        public UserService(AppDbContext db, IMapper mapper, IUserManagerWrapper userManagerWrapper)
        {
            _db = db;
            _mapper = mapper;
            _userManagerWrapper = userManagerWrapper;
        }

        public Guid GetUserIdByNameAsync(string name)
        {
            try
            {
                var id = _userManagerWrapper.FindIdByNameAsync(name);
                return id;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public PaginatedList<UserDTO> GetAllUsersAsync(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "")
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            currentFilter = searchString;

            var users = _db.Users
                .Where(t => t.IsDeleted == false)
                .Include(p => _mapper.Map<PlaylistDTO>(p))
                .WhereIf(!String.IsNullOrEmpty(searchString), s => s.UserName.Contains(searchString))
                .Select(t => _mapper.Map<UserDTO>(t));

            switch (sortOrder)
            {
                case "Name_desc":
                    users = users.OrderByDescending(b => b.UserName);
                    break;
                case "PlaylistsCount":
                    users = users.OrderBy(b => b.Playlists.Count);
                    break;
                case "PlaylistsCount_decs":
                    users = users.OrderByDescending(s => s.Playlists.Count);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 10;

            return PaginatedList<UserDTO>.Create(users.AsQueryable(), pageNumber ?? 1, pageSize);
        }

    }
}
