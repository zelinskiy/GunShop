using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models.Interfaces
{
    public interface IManufacturer
    {
        string Name { get; set; }
        string Country { get; set; }
    }
}
