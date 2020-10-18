using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.ViewModels
{
    public class OrderViewModel
    {
        public string Adress { get; set; }
        public int House { get; set; }
        public int Flat { get; set; }
        public int GoodsId { get; set; }

    }
    public class OrderModel
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public int House { get; set; }
        public int Flat { get; set; }
        public string GoodsName { get; set; }
        public string GoodsImage { get; set; }
    }
    public class ChangeOrderStatusViewModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }        
    }
}
