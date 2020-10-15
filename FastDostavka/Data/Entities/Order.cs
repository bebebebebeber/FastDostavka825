using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FastDostavka.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Addres { get; set; }
        public int House { get; set; }
        public int Flat { get; set; }
        [ForeignKey("Goods")]
        public int GoodsId { get; set; }
        public Goods Goods { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public UserProfile User { get; set; }

    }
}
