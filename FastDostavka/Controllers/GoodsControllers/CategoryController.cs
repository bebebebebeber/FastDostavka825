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

namespace FastDostavka.Controllers.GoodsControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        public CategoryController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpGet("categories")]
        public IActionResult Categories()
        {
            string app = "Hello test docker";
            return Ok(_context.Categories.Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name
            }));
        }
    }
}
