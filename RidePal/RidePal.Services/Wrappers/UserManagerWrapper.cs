﻿using Microsoft.AspNetCore.Identity;
using RidePal.Models;
using RidePal.Services.Wrappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidePal.Services.Wrappers
{
    public class UserManagerWrapper : IUserManagerWrapper
    {
        private readonly UserManager<User> userManager;

        public UserManagerWrapper(UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        public async Task<User> FindByNameAsync(string username)
        {
            return await this.userManager.FindByNameAsync(username);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await this.userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await this.userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<IList<string>> GetAllRoles(string userName)
        {
            User user = await this.userManager.FindByNameAsync(userName);

            return await this.userManager.GetRolesAsync(user);
        }

        public async Task<string> GetRole(string userName)
        {
            User user = await this.userManager.FindByNameAsync(userName);

            var role = (await this.userManager.GetRolesAsync(user)).FirstOrDefault();

            return role;
        }

        public Guid FindIdByNameAsync(string username)
        {
            var id = this.userManager.FindByNameAsync(username);
            return id.Result.Id;

        }
    }
}
