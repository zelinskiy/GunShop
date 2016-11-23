using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class CommodityInStorage
    {
        public int StorageId { get; set; }
        public int CommodityId { get; set; }
        public DateTime StoredAt { get; set; }
    }
}
