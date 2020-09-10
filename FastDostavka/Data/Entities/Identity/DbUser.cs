using FastDostavka.Data.Entities;
using JobJoin.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobJoin.Data.Entities.IdentityUser
{
    public class DbUser:IdentityUser<string>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
