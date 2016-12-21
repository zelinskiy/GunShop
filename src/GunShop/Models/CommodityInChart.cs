using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GunShop.Models
{
    public class CommodityInChart
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Key]
        public int CommodityId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("CommodityId")]
        public Commodity Commodity { get; set; }
    }
}
