using Microsoft.AspNetCore.Identity;
using RidePal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            db.Roles.Add(new Role
            {
                Name = "User",
            });
            db.SaveChanges();
        }
        public static void SeedUsersRolesUsers(this AppDbContext db)
        {

            var hasher = new PasswordHasher<User>();

            for (int i = 1; i <= 10; i++)
            {

                string image = $"/images/.jpg";
                string firstName = GetRandomFirstName();
                string lastName = GetRandomLastName();
                string password = firstName.First().ToString().ToUpper() + firstName.Substring(1).ToLowerInvariant().Replace(" ", "") + "@1";
                string email = (firstName + lastName).Replace(" ", "") + "@gmail.com";
                DateTime dateCreated = DateTime.UtcNow;
                if (db.Users.Any(x => x.Email == email))
                {
                    i--;
                }
                else
                {

                    db.Users.Add(new User
                    {
                        CreatedOn = dateCreated,
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true,
                        Image = image,
                        IsDeleted = false,
                        IsBanned = false,
                        IsAdmin = false,
                        LockoutEnabled = true,

                    });
                    db.SaveChanges();

                    User userCreated = db.Users.FirstOrDefault(x => x.Email == email);
                    userCreated.PasswordHash = hasher.HashPassword(userCreated, password);
                    db.SaveChanges();

                    db.UserRoles.Add(new IdentityUserRole<Guid>
                    {
                        RoleId = db.Roles.FirstOrDefault(x => x.Name == "User").Id,
                        UserId = userCreated.Id,
                    });
                    db.SaveChanges();
                }


            }

        }

        public static void SeedUsersRoleAdmin(this AppDbContext db)
        {

            var hasher = new PasswordHasher<User>();
            for (int i = 1; i < 10; i++)
            {



                db.Users.Add(new User
                {
                    CreatedOn = DateTime.UtcNow,
                    FirstName = "Gencho",
                    LastName = "Genev",
                    UserName = "gigenev@gmail.com",
                    Email = "gigenev@gmail.com",
                    EmailConfirmed = true,
                    Image = "/images/....jpg", // To set image,
                    IsDeleted = false,
                    IsBanned = false,
                    IsAdmin = true,
                    LockoutEnabled = false,

                });
                db.SaveChanges();

                var email = db.Users.FirstOrDefault(x => x.Email == "gigenev@gmail.com");
                email.PasswordHash = hasher.HashPassword(email, "");
                db.SaveChanges();

                db.UserRoles.Add(new IdentityUserRole<Guid>
                {
                    RoleId = db.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                    UserId = email.Id,
                });
                db.SaveChanges();

                db.Users.Add(new User
                {
                    CreatedOn = DateTime.UtcNow,
                    FirstName = "Dimitar",
                    LastName = "Dimitrov",
                    Email = "ddimitrov@gmail.com",
                    EmailConfirmed = true,
                    Image = "/images/.....jpg",
                    IsDeleted = false,
                    IsBanned = false,
                    IsAdmin = true,
                    LockoutEnabled = false,

                });
                db.SaveChanges();

                var emailDimitar = db.Users.FirstOrDefault(x => x.Email == "ddimitrov@gmail.com");
                emailDimitar.PasswordHash = hasher.HashPassword(emailDimitar, "@DDimitrov");
                db.SaveChanges();

                db.UserRoles.Add(new IdentityUserRole<Guid>
                {
                    RoleId = db.Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                    UserId = emailDimitar.Id,
                });
                db.SaveChanges();

            }


        }
        private static readonly Random random = new Random();

        private static readonly List<string> FirstNames = new List<string>()
            {
                "Gosho","Tosho","Siika","Jonny", "Dancho","Steve","Rocky","Tisho","Mark","Josh","Hristo"
            };

        private static readonly List<string> LastNames = new List<string>()
            {
                "Ivanov", "Petrov", "Todorov", "Dimitrov","Siderov","Zhelyazkov","Karadjov","Minkov","Moskov","Hristov"

            };


        private static string GetRandomFirstName()
        {

            string firstName = FirstNames[random.Next(0, FirstNames.Count)];

            return firstName;
        }

        private static string GetRandomLastName()
        {
            return LastNames[random.Next(0, LastNames.Count)];
        }

    }


}


