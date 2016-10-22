using GunShop.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services.Interfaces
{
    public interface ICommoditiesService
    {
        CommodityBO[] GetAllCommodities();
        CommodityTypeBO[] GetAllCommoditiesTypes();
    }
}
