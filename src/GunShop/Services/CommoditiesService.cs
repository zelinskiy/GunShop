using GunShop.Data;
using GunShop.Models;
using GunShop.Models.Interfaces;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.HomeViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services
{
    public class CommoditiesService: ICommoditiesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CommoditiesService(
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            this._context = context;
            _logger = loggerFactory.CreateLogger<CommoditiesService>();
        }

        public async Task AddCommodityType(ICommodityType ct)
        {

            var newtype = _context.CommoditiesTypes.Add(new CommodityType()
            {
                Model = ct.Model,
                Size = ct.Size,
                Weight = ct.Weight,
                ManufacturerId = ct.ManufacturerId
            });
            await _context.SaveChangesAsync();
            _logger.LogWarning($"Added Commiodity {ct.Model}");

            var commodityTypeViewModel = ct as CommodityTypeViewModel;
            if (commodityTypeViewModel == null) return;
            for (int i = 0; i < commodityTypeViewModel.InitialCount; i++)
            {
                _context.Commodities.Add(new Commodity { CommodityTypeId = newtype.Entity.Id });
            }
            await _context.SaveChangesAsync();
        }

        public CommodityBO[] GetAllCommodities()
        {
            return _context.Commodities.Join(
                _context.CommoditiesTypes,
                c => c.CommodityTypeId,
                ct => ct.Id,
                (c, ct) => new CommodityBO(c).AddType(ct)
            )
            .Join(
                _context.Manufacturers,
                cbo => cbo.ManufacturerId,
                m => m.Id,
                (cbo, m) => cbo.AddManufacturer(m)
            )
            .ToArray();
        }

        public CommodityTypeBO[] GetAllCommoditiesTypes()
        {
            return _context.CommoditiesTypes.Join(
                _context.Manufacturers,
                ct => ct.ManufacturerId,
                m => m.Id,
                (ct, m) => new CommodityTypeBO(ct)
                    .AddManufacturer(m)
                    .AddAvailableCount(_context.Commodities
                        .Where(c=>!_context.CommoditiesInCharts.Select(cic=>cic.CommodityId).Contains(c.Id))
                        .Count(c => c.CommodityTypeId == ct.Id))
                )
            .ToArray();
        }

        public bool HasAvailableCommodity(int commodityType)
        {
            return _context.Commodities.Count(c => c.CommodityTypeId == commodityType) > 0;
        }

        public Commodity GetAvailableCommodity(int commodityType)
        {
            return _context.Commodities.FirstOrDefault(c => c.CommodityTypeId == commodityType);
        }
    }
}
