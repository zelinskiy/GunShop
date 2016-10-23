using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GunShop.Models
{
    public class CommodityInChart
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Key]
        public int CommodityId { get; set; }
    }
}
