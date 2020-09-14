using Bogus;
using FastDostavka.Data.Entities;
using FastDostavka.Data.Entities.IdentityUser;
using FastDostavka.Data.Seed.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Seed
{
    public class PreConfigured
    {
        public static void SeedRoles(RoleManager<DbRole> roleManager)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    string roleName = "User";
                    var result = roleManager.CreateAsync(new DbRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    }).Result;
                    roleName = "Admin";
                    var result2 = roleManager.CreateAsync(new DbRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    }).Result;               
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static async Task SeedUsers(UserManager<DbUser> userManager, DBContext context)
        {
            try
            {
                if (!context.UserProfiles.Any())
                {
                    DbUser user1 = new DbUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "admin1",
                        Email = "beeadmin@gmail.com",
                        PhoneNumber = "+380503334031",
                    };
                    DbUser user2 = new DbUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "admin2",
                        Email = "slonik@gmail.com",
                        PhoneNumber = "+380505551541",
                    };


                    await userManager.CreateAsync(user1, "Qwerty-1");
                    await userManager.AddToRoleAsync(user1, "Admin");

                    await userManager.CreateAsync(user2, "Qwerty-1");
                    await userManager.AddToRoleAsync(user2, "Admin");



                    UserProfile profile1 = new UserProfile
                    {
                        Id = user1.Id,
                        FirstName = "Віктор",
                        LastName = "Дем’янюк",
                        City="Rovne",
                        //Surname="Вікторович",
                        Address = "вулиця Шевченка, 45",
                        RegisterDate = new DateTime(1983, 6, 23),
                        Image = "default.jpg",
                        LastLogined = DateTime.Now
                    };
                    UserProfile profile2 = new UserProfile
                    {
                        Id = user2.Id,
                        FirstName = "Лариса",
                        LastName = "Осадча",
                        City = "Rovne",
                        //Surname = "Костянтинівна",
                        Address = "вулиця Гранична, 58",
                        RegisterDate = new DateTime(1983, 6, 23),
                        Image = "default.jpg",
                        LastLogined = DateTime.Now
                    };

                    await context.UserProfiles.AddRangeAsync(profile1, profile2);
                    await context.SaveChangesAsync();
                }
                if (!userManager.GetUsersInRoleAsync("User").Result.Any())
                {
                    Faker<UserModel> usersFaked = new Faker<UserModel>("en")
                                    .RuleFor(t => t.RegisterDate, f => f.Date.BetweenOffset(
                                        new DateTimeOffset(DateTime.Now.AddYears(-10)),
                                        new DateTimeOffset(DateTime.Now.AddYears(-1))).DateTime)
                                    .RuleFor(t => t.Email, f => f.Person.Email)
                                    .RuleFor(t => t.UserName, f => f.Person.UserName)
                                    .RuleFor(t => t.PhoneNumber, f => f.Person.Phone)
                                    .RuleFor(t => t.Address, f => f.Address.FullAddress())
                                    .RuleFor(t => t.City, f => f.Address.City())
                                    .RuleFor(t => t.FirstName, f => f.Person.FirstName)
                                    .RuleFor(t => t.LastName, f => f.Person.LastName)
                                    .RuleFor(t => t.Age, f => f.Random.Int(0, 50));

                    var randoms = usersFaked.Generate(20);
                    foreach (var item in randoms)
                    {
                        DbUser user = new DbUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = item.UserName,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                        };
                        await userManager.CreateAsync(user, "Qwerty-1");
                        await userManager.AddToRoleAsync(user, "User");

                        UserProfile prof = new UserProfile
                        {
                            Id = user.Id,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Address = item.Address,
                            Image = "default.jpg",
                            City=item.City,
                            LastLogined = DateTime.Now,
                            RegisterDate = item.RegisterDate,
                            Age = item.Age
                        };
                        await context.UserProfiles.AddAsync(prof);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            

        }
    }
}
