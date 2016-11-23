using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Models;

namespace GunShop.ViewModels.CommodityViewModels
{
    public class AllCommodityTypesViewModel
    {
        public IEnumerable<CommodityTypeBO> CommoditiesTypes { get; set; }
        public IEnumerable<Category> SubCategories { get; set; }
    }
}
