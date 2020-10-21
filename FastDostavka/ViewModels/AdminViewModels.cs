using FastDostavka.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.ViewModels
{
    public class AdminOrdersViewModel
    {
        public List<OrderModel> Orders { get; set; }
        public List<OrderStatus> Statuses { get; set; }
        public int Pages { get; set; }
    }
    public class AdminAddProductViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Decription { get; set; }
        public double Price { get; set; }
        public int StoreId { get; set; }
    }
    public class AdminGetStoresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
