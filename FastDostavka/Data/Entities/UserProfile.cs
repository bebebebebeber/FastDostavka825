using FastDostavka.Data.Entities.IdentityUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
        public string RefreshToken { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLogined { get; set; }

        public virtual DbUser DbUser { get; set; }
    }
}
