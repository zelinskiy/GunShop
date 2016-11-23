using GunShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.CommodityViewModels
{

    public class CharacteristicPreview
    {
        public int CharacteristicId { get; set; }
        public string CharacteristicName { get; set; }
        public string Value { get; set; }
        public int CommodityTypeId { get; set; }
    }

    public class CommodityTypeBO:CommodityType
    {
        public string ManufacturerName { get; set; }
        public string ManufacturerCountry { get; set; }
        public ManufacturerPreview ManufacturerPreview { get; set; }
        public IEnumerable<CharacteristicPreview> CharacteristicPreviews { get; set; }

        public int AvailableCount { get; set; }
        
        public CommodityTypeBO(CommodityType t)
        {
            this.Id = t.Id;
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


        public CommodityTypeBO AddCharacteristics(IEnumerable<CharacteristicPreview> characteristicPreviews)
        {
            this.CharacteristicPreviews = characteristicPreviews;

            return this;
        }

    }
}
