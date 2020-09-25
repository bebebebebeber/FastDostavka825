using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Data.Entities.IdentityUser;
using FastDostavka.Services;
using FastDostavka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastDostavka.Controllers.UserControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpGet("profile")]
        public  IActionResult Profile()
        {
            var userId = User.Claims.ToList()[0].Value;
            var profile = _context.UserProfiles.FirstOrDefault(x => x.Id == userId);
            return Ok(new ProfileViewModel()
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age ?? 0 ,
                Ardess = profile.Address,
                Image = profile.Image,
                City = profile.City
            });
        }
    }
}
