using GunShop.Models;
using GunShop.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.LocationViewModels
{
    public class ShippingViewModel
    {
        public IEnumerable<CommodityBO> Commodities { get; set; }
        public Storage StorageA { get; set; }
        public int StorageAId { get; set; }

        public IEnumerable<Storage> StorageBCandidates { get; set; }
        public int StorageBId { get; set; }
    }
}
