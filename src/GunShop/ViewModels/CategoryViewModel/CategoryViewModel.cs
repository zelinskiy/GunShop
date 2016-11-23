using GunShop.Models;
using GunShop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.CategoryViewModels
{
    public class CategoryViewModel : ICategory
    {
        public int Id { get; set; }
        public int? MasterCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Characteristic> Characteristics { get; set; }

        public IEnumerable<ICategory> AllCategories { get; set; }
    }
}
