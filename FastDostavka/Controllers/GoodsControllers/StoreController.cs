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
    public class StoreController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        public StoreController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("stores")]
        public IActionResult Stores([FromBody]StoreViewModel model)
        {
            try
            {
                if (model.Category != 0)
                {
                    return Ok(_context.Stores.Where(x => x.Category.Id == model.Category).Select(x => new StoreModel()
                    {
                        Id = x.Id,
                        Adress = x.Adress,
                        Description = x.Description,
                        Image = x.Image,
                        CategoryId = x.CategoryId,
                        Name = x.Name,
                        Coordinate1 = x.Coordinate1,
                        Coordinate2 = x.Coordinate2
                    }));
                }
                else
                {
                    return Ok(_context.Stores.Where(x => x.Category.Id == 1).Select(x => new StoreModel()
                    {
                        Id = x.Id,
                        Adress = x.Adress,
                        Description = x.Description,
                        Image = x.Image,
                        CategoryId = x.CategoryId,
                        Name = x.Name,
                        Coordinate1 = x.Coordinate1,
                        Coordinate2 = x.Coordinate2
                    }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("goods")]
        public IActionResult Goods([FromBody] GoodsViewModel model)
        {
            try
            {
                return Ok(_context.Goods.Where(x => x.Store.Id == model.Id).Select(x => new GoodsModel()
                {
                    Id = x.Id,
                    Description = x.Decription,
                    Image = x.Image,
                    Name = x.Name,
                    Price = x.Price
                }));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
