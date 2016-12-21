using GunShop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class CommodityType:ICommodityType
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public int Weight { get; set; }
        public int ManufacturerId { get; set; }
        
        //public ICollection<CharacteristicValue> CharacteristicValues { get; set; }
    }
}
