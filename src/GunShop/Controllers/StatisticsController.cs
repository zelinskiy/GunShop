using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Services;
using GunShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GunShop.Controllers
{
    public class StatisticsController : Controller
    {

        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;
        ILogger _logger;

        public StatisticsController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            ICommoditiesService commoditiesService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
            _logger = loggerFactory.CreateLogger<StatisticsController>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChartSizes()
        {
            var result = _context.CommoditiesInCharts
                .ToArray()
                .GroupBy(cic => cic.CustomerId)
                .Select(gr => new ChartSize
                {
                    CustomerId = gr.Key,
                    Count = gr.Count()
                });

            return Json(result);
        }

        [HttpGet]
        public IActionResult StoragesFilled()
        {
            var result = _context.Commodities
                .ToArray()
                .GroupBy(c => c.StorageId)
                .Select(gr => new StorageFillment
                {
                    StorageId = gr.Key,
                    Count = gr.Count()
                });

            return Json(result);
        }

        [HttpGet]
        public IActionResult ManufacturersFilled()
        {
            var result = _context.Commodities
                .ToArray()
                .Join(_context.CommoditiesTypes,
                    comm => comm.CommodityTypeId,
                    ct => ct.Id,
                    (comm, ct) => ct.ManufacturerId
                ).GroupBy(mid => mid)
                .Select(gr => new ManufacturerFillment
                {
                    ManufacturerId = gr.Key,
                    Count = gr.Count()
                });

            return Json(result);
        }
    }

    public struct ChartSize
    {
        public int CustomerId { get; set; }
        public int Count { get; set; }
    }

    public struct StorageFillment
    {
        public int StorageId { get; set; }
        public int Count { get; set; }
    }

    public struct ManufacturerFillment
    {
        public int ManufacturerId { get; set; }
        public int Count { get; set; }
    }
}