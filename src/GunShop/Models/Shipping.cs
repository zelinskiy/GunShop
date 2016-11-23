using System;
using System.Collections.Generic;
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
    }
}
