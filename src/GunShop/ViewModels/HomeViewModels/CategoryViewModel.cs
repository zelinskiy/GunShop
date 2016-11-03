using GunShop.Models;
using GunShop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.HomeViewModels
{
    public class CategoryViewModel : ICategory
    {
        public int Id { get; set; }
        public int? MasterCategoryId { get; set; }
        public string Name { get; set; }

        public Characteristic[] Characteristics { get; set; }
    }
}
