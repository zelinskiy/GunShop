using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models.Interfaces
{
    public interface ICommodityType
    {
        string Model { get; set; }
        string Size { get; set; }
        int Weight { get; set; }
        int ManufacturerId { get; set; }
    }
}
