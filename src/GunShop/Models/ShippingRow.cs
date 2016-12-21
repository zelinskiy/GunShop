using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class ShippingRow
    {
        [Key]
        public int ShippingId { get; set; }

        [Key]
        public int CommodityId { get; set; }

        [ForeignKey("ShippingId")]
        public Shipping Shipping { get; set; }

        [ForeignKey("CommodityId")]
        public Commodity Commodity { get; set; }

    }
}
