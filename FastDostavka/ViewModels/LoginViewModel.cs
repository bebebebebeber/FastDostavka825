using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class TokensModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
