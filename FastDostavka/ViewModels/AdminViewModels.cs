using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.ViewModels
{
    public class AdminOrdersViewModel
    {
        public List<OrderModel> Orders { get; set; }
        public int Pages { get; set; }
    }
}
