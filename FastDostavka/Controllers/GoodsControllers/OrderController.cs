using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Data.Entities;
using FastDostavka.Data.Entities.IdentityUser;
using FastDostavka.Hubs;
using FastDostavka.Services;
using FastDostavka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FastDostavka.Controllers.GoodsControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        private IHubContext<ChatHub> _hub;

        public OrderController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("change-order-status")]
        public async Task<IActionResult> Stores(ChangeOrderStatusViewModel model)
        {
            try
            {
                var userId = User.Claims.ToList()[0].Value;
                var o = _context.Orders.FirstOrDefault(x => x.Id == model.Id);
                o.OrderStatusId=model.StatusId;
                await _context.SaveChangesAsync();
                await _hub.Clients.User(o.UserId).SendAsync("orderStatusChanged", model.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("order")]
        public async Task<IActionResult> Stores([FromBody] OrderViewModel model)
        {
            try
            {
                var userId = User.Claims.ToList()[0].Value;
                Order o = new Order()
                {
                    Addres = model.Adress,
                    Flat = model.Flat,
                    House = model.House,
                    GoodsId = model.GoodsId,
                    UserId = userId,
                    OrderDate = DateTime.Now
                };
                _context.Orders.Add(o);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("orders")]
        public IActionResult Orders()
        {
            try
            {
                var userId = User.Claims.ToList()[0].Value;                
                return Ok(_context.Orders.Where(x => x.UserId == userId).OrderByDescending(x=>x.OrderDate).Select(x => new OrderModel()
                {
                    Id = x.Id,
                    Adress = x.Addres,
                    Flat = x.Flat,
                    House = x.House,
                    GoodsName = x.Goods.Name,
                    GoodsImage = x.Goods.Image
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
