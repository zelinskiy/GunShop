using GunShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.CommodityViewModels
{
    public class CommodityBO:Commodity
    {
        public string Model { get; set; }
        public string Size { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }
        public string ManufacturerCountry { get; set; }

        public CommodityBO(Commodity c)
        {
            this.Id = c.Id;
            this.CommodityTypeId = c.CommodityTypeId;
            this.OrderId = c.OrderId;
            this.StorageId = c.StorageId;
        }

        public CommodityBO AddType(CommodityType t)
        {
            this.Model = t.Model;
            this.Price = t.Price;
            this.Size = t.Size;
            this.Weight = t.Weight;
            this.ManufacturerId = t.ManufacturerId;

            return this;
        }

        public CommodityBO AddManufacturer(Manufacturer m)
        {
            this.ManufacturerCountry = m.Country;
            this.ManufacturerId = m.Id;
            this.ManufacturerName = m.Name;

            return this;
        }
        
    }
}
