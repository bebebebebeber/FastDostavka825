using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Decription { get; set; }
        public double Price { get; set; }
        public string StoreName { get; set; }
    }
}
