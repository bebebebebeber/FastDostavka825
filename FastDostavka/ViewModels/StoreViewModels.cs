using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.ViewModels
{
    public class StoreViewModel
    {
        public int Category { get; set; }
    }
    public class StoreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public double Coordinate1 { get; set; }
        public double Coordinate2 { get; set; }
    }
    public class GoodsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class GoodsViewModel
    {
        public int Id { get; set; }
    }
}
