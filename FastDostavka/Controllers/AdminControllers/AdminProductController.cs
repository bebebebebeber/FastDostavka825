using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Data.Entities.IdentityUser;
using FastDostavka.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FastDostavka.Data.Entities;
using FastDostavka.ViewModels;

namespace FastDostavka.Controllers.AdminControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminProductController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public AdminProductController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductAsync([FromBody] AdminAddProductViewModel model)
        {
            try
            {
                await _context.Goods.AddAsync(new Goods
                {
                    Name = model.Name,
                    Image = model.Image,
                    Decription = model.Decription,
                    StoreId = model.StoreId,
                    Price = model.Price
                });

                _context.SaveChanges();
                return Ok("Successfully added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
