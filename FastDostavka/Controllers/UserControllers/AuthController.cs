using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Services;
using FastDostavka.ViewModels;
using JobJoin.Data.Entities.IdentityUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastDostavka.Controllers.UserControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = _context.Users.Include(u => u.UserProfile).FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                return BadRequest("Не правильна електронна пошта!");
            }
            var res = _signInManager
                .PasswordSignInAsync(user, model.Password, false, false).Result;
            if (!res.Succeeded)
            {
                return BadRequest("Не правильний пароль!");
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            //var refreshToken = _jwtTokenService.GenerateRefreshToken();
            user.UserProfile.LastLogined = DateTime.Now;
            _context.SaveChanges();

            return Ok(new { token = _jwtTokenService.CreateToken(user)});
        }
    }
}
