using FastDostavka.Data;
using JobJoin.Data.Entities.IdentityUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FastDostavka.Services
{
    public interface IJwtTokenService
    {
        string CreateToken(DbUser user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateToken(List<Claim> claims);
        string GenerateRefreshToken();
    }
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly DBContext _context;
        private readonly IConfiguration _configuration;
        public JwtTokenService(UserManager<DbUser> userManager, DBContext context,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }
        public string CreateToken(DbUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            roles = roles.OrderBy(x => x).ToList();
            var image = user.UserProfile.Image;

            if (image == null)
            {
                image = _configuration.GetValue<string>("DefaultImage");
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.Id),
                new Claim("name",user.UserName),
                new Claim("email",user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("image",image),
                new Claim("emailConfirmed",user.EmailConfirmed.ToString()),
                new Claim("phone",user.PhoneNumber)
            };
            foreach (var el in roles)
            {
                claims.Add(new Claim("roles", el));
            }
            //var now = DateTime.UtcNow;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c672421d-af28-445b-b68a-01a2040ab899"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddMinutes(3),
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public string GenerateToken(List<Claim> claims)
        {
            List<Claim> c = new List<Claim>();
            foreach (var el in claims)
            {
                if (el.Type == ClaimTypes.Email)
                {
                    c.Add(new Claim("email", el.Value));
                }
                else if (el.Type == ClaimTypes.Role)
                {
                    c.Add(new Claim("roles", el.Value));
                }
                else
                {
                    c.Add(el);
                }
            }
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c672421d-af28-445b-b68a-01a2040ab899"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddMinutes(3),
                claims: c
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public string GenerateRefreshToken()
        {
            //var randomNumber = new byte[32];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(randomNumber);
            //    return Convert.ToBase64String(randomNumber);
            //}

            return Guid.NewGuid().ToString();
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c672421d-af28-445b-b68a-01a2040ab899")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
