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
        public int ShippingId { get; set; }
        public string AuthorId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<CommodityBO> Commodities { get; set; }
        public Storage StorageA { get; set; }
        public Storage StorageB { get; set; }
    }
}
