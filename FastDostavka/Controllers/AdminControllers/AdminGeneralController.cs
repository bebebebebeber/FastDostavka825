using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Data.Entities.IdentityUser;
using FastDostavka.Services;
using FastDostavka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FastDostavka.Controllers.AdminControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminGeneralController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public AdminGeneralController(IWebHostEnvironment env, IConfiguration configuration,DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _env = env;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpGet("stores")]
        public IActionResult GetStores()
        {
            try
            {
                var res = _context.Stores.Select(t => new AdminGetStoresViewModel
                {
                    Id = t.Id,
                    Name = t.Name + ", " + t.Adress,
                    Image = t.Image
                });
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }     
        }
    }
}
