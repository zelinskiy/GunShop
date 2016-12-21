using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("CommodityTypeId")]
        public CommodityType CommodityType { get; set; }
        [ForeignKey("StorageId")]
        public Storage Storage { get; set; }
    }
}
