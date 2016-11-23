using GunShop.Models;
using GunShop.Models.Interfaces;
using GunShop.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services.Interfaces
{
    public interface ICommoditiesService
    {
        IEnumerable<CommodityBO> GetAllCommodities();
        IEnumerable<CommodityTypeBO> GetAllCommoditiesTypes();

        Commodity GetAvailableCommodity(int commodityType);
        bool HasAvailableCommodity(int commodityType);

        Task AddCommodityType(ICommodityType ct);
        Task AddManufacturer(IManufacturer man);
    }
}
