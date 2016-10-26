using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Models.Interfaces;

namespace GunShop.ViewModels.HomeViewModels
{
    public class ManufacturerViewModel : IManufacturer
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
