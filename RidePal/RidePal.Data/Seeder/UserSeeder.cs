using Microsoft.AspNetCore.Identity;
using RidePal.Models;
using System;
using System.Linq;


namespace RidePal.Data.Seeder
{

    public static class UserSeeder

    {
        public static void SeedRoles(this AppDbContext db)
        {
            db.Roles.Add(new Role
            {
                Name = "Admin"
            });
            db.SaveChanges();
        }


        public static void SeedUsersRoleAdmin(this AppDbContext db)
        {
            var hasher = new PasswordHasher<User>();


            db.Users.Add(new User
            {
                CreatedOn = DateTime.UtcNow,
                FirstName = "Gencho",
                LastName = "Genev",
                UserName = "gigenev@gmail.com",
                NormalizedUserName = "GIGENEV@ADMIN.COM",
                Email = "gigenev@gmail.com",
                NormalizedEmail = "GIGENEV@GMAIL.COM",
                EmailConfirmed = true,
                Image = "~/images/Profile.jpg", // To set image,
                IsDeleted = false,
                IsBanned = false,
                IsAdmin = true,
                LockoutEnabled = false,

            });
            db.SaveChanges();

            var gencho = db.Users.FirstOrDefault(x => x.Email == "gigenev@gmail.com");
            gencho.PasswordHash = hasher.HashPassword(gencho, "Gencho12345!@");
            db.SaveChanges();

            db.UserRoles.Add(new IdentityUserRole<Guid>
            {
                RoleId = db.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                UserId = gencho.Id,
            });
            db.SaveChanges();

            db.Users.Add(new User
            {
                CreatedOn = DateTime.UtcNow,
                FirstName = "Dimitar",
                LastName = "Dimitrov",
                UserName = "ddimitrov@gmail.com",
                NormalizedUserName = "DDIMITROV@GMAIL.COM",
                Email = "ddimitrov@gmail.com",
                NormalizedEmail = "DDIMITROV@GMAIL.COM",
                EmailConfirmed = true,
                Image = "/images/.....jpg",
                IsDeleted = false,
                IsBanned = false,
                IsAdmin = true,
                LockoutEnabled = false,

            });
            db.SaveChanges();

            var dimitar = db.Users.FirstOrDefault(x => x.Email == "ddimitrov@gmail.com");
            dimitar.PasswordHash = hasher.HashPassword(dimitar, "Dimitar12345!@");
            db.SaveChanges();

            db.UserRoles.Add(new IdentityUserRole<Guid>
            {
                RoleId = db.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                UserId = dimitar.Id,
            });
            db.SaveChanges();

        }


    }

}


