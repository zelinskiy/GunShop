using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Characteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public string AvailableValues { get; set; }
    }
}
