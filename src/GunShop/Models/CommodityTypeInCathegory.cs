using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class CommodityTypeInCathegory
    {
        [Key]
        public int CommodityTypeId { get; set; }

        [Key]
        public int CategoryId { get; set; }

        [ForeignKey("CommodityTypeId")]
        public CommodityType CommodityType { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
