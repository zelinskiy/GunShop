using GunShop.Data;
using GunShop.Models;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.HomeViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services
{
    public class ChartService: IChartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ChartService(
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ChartService>();
        }

        public void AddToChart(int customerId, int commodityTypeId)
        {
            if (!(_context.Commodities.Count(c=>c.CommodityTypeId == commodityTypeId) > 0))
            {
                throw new ArgumentException($"No commodities of type {commodityTypeId} available");
            }
            else if(_context.Customers.FirstOrDefault(c=>c.Id == customerId) == null)
            {
                throw new ArgumentException($"Customer {customerId} not found");
            }
            _context.CommoditiesInCharts.Add(new CommodityInChart {
                CustomerId = customerId,
                CommodityId = getFirstAvailable(commodityTypeId).Id
            });
            _context.SaveChanges();
            _logger.LogInformation($"Commodity {commodityTypeId} added to Chart {customerId}");
        }

        private Commodity getFirstAvailable(int commodityTypeId)
        {
            var commoditiesOnChartsIds = _context.CommoditiesInCharts
                .Select(cic => cic.CommodityId)
                .ToArray();

            return _context.Commodities
                .First(c => 
                    (!commoditiesOnChartsIds.Contains(c.Id))
                    && c.CommodityTypeId == commodityTypeId);
        }


        public CommodityBO[] OnChart(int customerId)
        {

            var myCommoditiesIds = _context.CommoditiesInCharts
                .Where(cic => cic.CustomerId == customerId)
                .Select(cic => cic.CommodityId)
                .ToArray();

            return _context.Commodities
            .Where(c=> myCommoditiesIds.Contains(c.Id))
            .Join(
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

        public void RemoveFromChart(int customerId, int commodityId)
        {
            var toBeRemoved = _context.CommoditiesInCharts
                .FirstOrDefault(cic => cic.CommodityId == commodityId 
                && cic.CustomerId == customerId);
            if(toBeRemoved == null)
            {
                throw new ArgumentException($"Can't find item {commodityId} on chart {customerId}");
            }
            _context.CommoditiesInCharts.Remove(toBeRemoved);
            _context.SaveChanges();
        }

        public void ClearAllAnonymousCharts()
        {
            var anonymousUsersIds = _context.Customers
                .Where(cust => cust.IsTemp)
                .Select(cust=>cust.Id)
                .ToArray();

            var toBeRemoved = _context.CommoditiesInCharts
               .Where(cic => anonymousUsersIds.Contains(cic.CustomerId))
               .ToArray();
            _context.CommoditiesInCharts.RemoveRange(toBeRemoved);
            _context.SaveChanges();
        }
    }
}
