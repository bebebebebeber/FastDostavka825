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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public AdminProductController(IWebHostEnvironment env, IConfiguration configuration, DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _env = env;
            _configuration = configuration;
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
                string img = null;
                if (!string.IsNullOrEmpty(model.Image))
                {
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    string pathSaveImages = InitStaticFiles
                        .CreateImageByFileName(_env, _configuration,
                        new string[] { "ImagesPath", "ImagesUserPath" },
                        imageName, model.Image);
                    img = "500_" + imageName;
                }
                await _context.Goods.AddAsync(new Goods
                {
                    Name = model.Name,
                    Image = img,
                    Decription = model.Description,
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
