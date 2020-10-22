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
        public string Description { get; set; }
        public double Price { get; set; }
        public int StoreId { get; set; }
    }
    public class AdminGetStoresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class StoresList
    {
        public List<Category> Categories { get; set; }
        public List<AdminGetStoresViewModel> Stores { get; set; }

    }
    public class CreateStoreViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description{ get; set; }
        public double Coordinate1 { get; set; }
        public double Coordinate2 { get; set; }
        public int CategoryId { get; set; }
    }
    public class AdminProductsViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int Pages { get; set; }
    }
}
