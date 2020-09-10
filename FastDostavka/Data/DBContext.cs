using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobJoin.Data.Entities.IdentityUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FastDostavka.Data.Entities;
using FastDostavka.Data.Configurations;

namespace FastDostavka.Data
{
    public class DBContext : IdentityDbContext<DbUser, DbRole, string, IdentityUserClaim<string>,
   DbUserRole, IdentityUserLogin<string>,
   IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        }
    }
}
