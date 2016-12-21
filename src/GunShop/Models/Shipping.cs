using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Shipping
    {
        public int Id { get; set; }

        public int StorageAId { get; set; }
        public int StorageBId { get; set; }

        public DateTime Date { get; set; }
        public string AuthorId { get; set; }

        [ForeignKey("StorageAId")]
        public Storage StorageA { get; set; }

        [ForeignKey("StorageBId")]
        public Storage StorageB { get; set; }
    }
}
