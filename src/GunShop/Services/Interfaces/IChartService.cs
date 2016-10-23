using GunShop.Models;
using GunShop.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services.Interfaces
{
    public interface IChartService
    {
        void AddToChart(int customerId, int commodityTypeId);
        void RemoveFromChart(int customerId, int commodityId);
        void ClearAllAnonymousCharts();
        CommodityBO[] OnChart(int customerId);
    }
}
