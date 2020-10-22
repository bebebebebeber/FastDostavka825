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
        [HttpGet("get-products/{page}")]
        public IActionResult GetProducts(int page)
        {
            try
            {
                var products = _context.Goods.Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name=x.Name,
                    Price=x.Price,
                    Decription=x.Decription,
                    Image=x.Image,
                    StoreName = x.Store.Name
                });
                double t = products.Count();
                double c = t / 10;
                int count = int.Parse(Math.Ceiling(c).ToString());
                if (count < page)
                {
                    page = 1;
                }
                page -= 1;
                var res = products.Skip(page * 10).Take(10).ToList();
                return Ok(new AdminProductsViewModel()
                {
                    Products = res,
                    Pages = count
                });
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
                _context.Goods.Remove(_context.Goods.FirstOrDefault(x => x.Id == id));
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
