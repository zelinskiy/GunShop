using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public bool IsTemp { get; set; }
        public string Email { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
