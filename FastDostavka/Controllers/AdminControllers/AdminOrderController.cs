using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastDostavka.Data;
using FastDostavka.Data.Entities.IdentityUser;
using FastDostavka.Hubs;
using FastDostavka.Services;
using FastDostavka.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FastDostavka.Controllers.AdminControllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly DBContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IHubContext<ChatHub> _hub;

        public AdminOrderController(DBContext context, UserManager<DbUser> userManager, SignInManager<DbUser> sigInManager,
            IJwtTokenService jwtTokenService, IHubContext<ChatHub> hub)
        {
            _userManager = userManager;
            _signInManager = sigInManager;
            _context = context;
            _jwtTokenService = jwtTokenService;
            _hub = hub;
        }
        [HttpGet("orders/{page}")]
        public IActionResult Orders(int page)
        {
            try
            {
                var orders = _context.Orders.OrderByDescending(x => x.OrderDate).Select(x => new OrderModel()
                {
                    Id = x.Id,
                    Adress = x.Addres,
                    Flat = x.Flat,
                    House = x.House,
                    Status = x.OrderStatus.Name,
                    GoodsName = x.Goods.Name,
                    GoodsImage = x.Goods.Image
                });
                double t = orders.Count();
                double c = t / 10;                
                int count = int.Parse(Math.Ceiling(c).ToString());
                if (count < page)
                {
                    page = 1;
                }
                page -= 1;
                var res = orders.Skip(page * 10).Take(10).ToList();
                return Ok(new AdminOrdersViewModel()
                { 
                    Orders = res,
                    Pages = count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
