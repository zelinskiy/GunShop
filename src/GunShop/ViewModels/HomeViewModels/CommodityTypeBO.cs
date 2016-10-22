using GunShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.HomeViewModels
{
    public class CommodityTypeBO:CommodityType
    {
        public string ManufacturerName { get; set; }
        public string ManufacturerCountry { get; set; }
        public ManufacturerPreview ManufacturerPreview { get; set; }

        public int AvailableCount { get; set; }
        
        public CommodityTypeBO(CommodityType t)
        {
            this.Model = t.Model;
            this.Size = t.Size;
            this.Weight = t.Weight;
            this.ManufacturerId = t.ManufacturerId;            
        }

        public CommodityTypeBO AddManufacturer(Manufacturer m)
        {
            this.ManufacturerCountry = m.Country;
            this.ManufacturerId = m.Id;
            this.ManufacturerName = m.Name;
            this.ManufacturerPreview = new ManufacturerPreview(m);

            return this;
        }

        public CommodityTypeBO AddAvailableCount(int n)
        {
            this.AvailableCount = n;

            return this;
        }
    }
}
