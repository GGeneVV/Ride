using RidePal.Services.DTOModels;
using RidePal.Services.Pagination;
using System;

namespace RidePal.Services.Contracts
{
    public interface IUserService
    { 
        public string Authenticate(string username, string password);
        public Guid GetUserIdByNameAsync(string name);
        public PaginatedList<UserDTO> GetAllUsersAsync(int? pageNumber = 1,
            string sortOrder = "",
            string currentFilter = "",
            string searchString = "");
    }
}
