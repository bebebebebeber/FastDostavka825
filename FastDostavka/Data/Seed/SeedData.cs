using FastDostavka.Data.Entities.IdentityUser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Seed
{
    public class SeedData
    {
        public static async Task Seed(IServiceProvider services, IWebHostEnvironment env, IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<DBContext>();
                //context.Database.Migrate();
                PreConfigured.SeedRoles(managerRole);
                await PreConfigured.SeedUsers(manager, context);
            }
        }
    }
}
