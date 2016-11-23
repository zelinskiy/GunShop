using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Commodity
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int CommodityTypeId { get; set; }
        public int StorageId { get; set; }      
    }
}
