using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class CommodityInStorage
    {
        [Key]
        public int StorageId { get; set; }

        [Key]
        public int CommodityId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime StoredAt { get; set; }
    }
}
