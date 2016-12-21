using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Coordinates { get; set; }

        [ForeignKey("StorageId")]
        public Storage Storage { get; set; }
    }
}
