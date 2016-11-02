using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Models;
using Microsoft.Extensions.Logging;

namespace GunShop.Services
{
    public class CategorizationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CategorizationService(
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ChartService>();
        }

        public void AddCommodityTypeToCategory(CommodityType ct, Category cat)
        {
            var newConnection = new CommodityTypeInCathegory
            {
                CategoryId = cat.Id,
                CommodityTypeId = ct.Id
            };
            _context.CommoditiesTypesInCathegories.Add(newConnection);
            _context.SaveChanges();
        }

        public void RemoveCommodityTypeFromCategory(CommodityType ct, Category cat)
        {
            var oldConnection =
                _context.CommoditiesTypesInCathegories.FirstOrDefault(
                    conn => conn.CommodityTypeId == ct.Id && conn.CategoryId == cat.Id);
            if (oldConnection == null)
            {
                throw new ArgumentException($"Commodity {ct.Id} not in Category {cat.Id}");
            }
            _context.CommoditiesTypesInCathegories.Remove(oldConnection);
            _context.SaveChanges();
        }
    }
}
