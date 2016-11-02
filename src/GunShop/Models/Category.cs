using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int MasterCategoryId { get; set; }
        public string Name { get; set; }
    }
}
