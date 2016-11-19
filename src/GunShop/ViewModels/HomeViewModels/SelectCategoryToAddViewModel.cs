using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Models;

namespace GunShop.ViewModels.HomeViewModels
{
    public class SelectCategoryToAddViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public int CommodityTypeId;
    }
}
