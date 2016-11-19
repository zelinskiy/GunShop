using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Models;

namespace GunShop.ViewModels.HomeViewModels
{
    public class AddCommodityTypeToCategoryViewModel
    {
        public int CommodityTypeId { get; set; }
        public string CommodityTypeModel { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<Characteristic> Characteristics { get; set; }

        public IEnumerable<CharacteristicValue> CharacteristicsValues { get; set; }

    }
}
