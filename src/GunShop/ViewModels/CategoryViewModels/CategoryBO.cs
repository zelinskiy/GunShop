using GunShop.Models;
using GunShop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.CategoryViewModels
{
    public class CategoryBO:ICategory
    {
        public int Id { get; set; }
        public int? MasterCategoryId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Characteristic> Characteristics { get; set; }
    }
}
