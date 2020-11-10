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

        public Task<UserDTO> EditUser(Guid userId)
        {
            try
            {
               
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task DeleteUser(Guid userId)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
