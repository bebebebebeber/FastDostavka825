using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Data.Entities;
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
    public class AdminStoresController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        public AdminStoresController(IWebHostEnvironment env, IConfiguration configuration, DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _env = env;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("create-store")]
        public IActionResult CreateStore([FromBody]CreateStoreViewModel model)
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
                    img = imageName;
                }
                Store s = new Store() 
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Adress = model.Coordinate1.ToString(),
                    Coordinate1 = model.Coordinate1,
                    Coordinate2 = model.Coordinate2,
                    Image = img
                };
                _context.Stores.Add(s);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _context.Stores.Remove(_context.Stores.FirstOrDefault(x => x.Id == id));
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
