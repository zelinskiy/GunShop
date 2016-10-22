using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class CommodityType
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public int Weight { get; set; }
        public int ManufacturerId { get; set; }
    }
}
