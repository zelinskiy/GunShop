using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class CharacteristicValue
    {
        public string Value { get; set; }

        [Key]
        public int CharacteristicId { get; set; }

        [Key]
        public int CommodityTypeId { get; set; }
    }
}
