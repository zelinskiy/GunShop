using GunShop.Models;
using System;
using System.Collections.Generic;

namespace GunShop.ViewModels.HomeViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Commodity> Commodities { get; set; }
    }
}
