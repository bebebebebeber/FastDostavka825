using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastDostavka.Controllers.GamesControllers
{
    public class PosVextor3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
    public class Solider
    {
        public string Nick { get; set; }
        public PosVextor3 Pos { get; set; }
        public PosVextor3 Velocity { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CarsConroller : ControllerBase
    {
        private static List<Solider> list = new List<Solider>();
        //Отримуємо дані про постріл
        [HttpGet("{nick}")]
        public IActionResult Index(string nick)
        {
            var solider = list.SingleOrDefault(s => s.Nick == nick);
            if (solider != null)
                list.Remove(solider);
            return Ok(solider);
        }
        //Стриляємо по обєкту
        [HttpPost]
        public IActionResult Post([FromBody] Solider solider)
        {
            if (solider != null)
                list.Add(solider);
            return Ok();
        }

    }
}
