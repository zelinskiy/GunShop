using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Commodity
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int CommodityTypeId { get; set; }

        public string Model;
        public string Size;
        public int Weight;

        public Commodity() { }

        public Commodity(Commodity old, CommodityType ctype)
        {
            Id = old.Id;
            CustomerId = old.CustomerId;
            CommodityTypeId = old.CommodityTypeId;
            Model = ctype.Model;
            Size = ctype.Size;
            Weight = ctype.Weight;
        }

        
    }
}
