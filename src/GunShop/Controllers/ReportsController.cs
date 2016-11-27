using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Services.Interfaces;
using GunShop.Utils;
using GunShop.ViewModels.LocationViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GunShop.Controllers
{
    public class ReportsController : Controller
    {

        ApplicationDbContext _context;
        ILogger _logger;
        ICommoditiesService _commoditiesService;
        private readonly IHostingEnvironment _appEnvironment;

        public ReportsController(
            ApplicationDbContext context,
            IHostingEnvironment appEnvironment,
            ICommoditiesService commoditiesService,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ReportsController>();
            _commoditiesService = commoditiesService;
            _appEnvironment = appEnvironment;
        }

        public IActionResult ShippmentReportPdf(int id)
        {
            var shipping = _context.Shippings.FirstOrDefault(s => s.Id == id);

            if (shipping == null)
            {
                return Content("Not Found");
            }

            var commoditiesInShipping = _context.ShippingRows
                .Where(sr => sr.ShippingId == id)
                .Select(sr => sr.CommodityId)
                .ToArray();

            var model = new ShippingViewModel
            {
                ShippingId = shipping.Id,
                AuthorId = shipping.AuthorId,
                Date = shipping.Date,
                StorageA = _context.Storages.First(st => st.Id == shipping.StorageAId),
                StorageB = _context.Storages.First(st => st.Id == shipping.StorageBId),
                Commodities = _commoditiesService.GetAllCommodities()
                    .Where(c => commoditiesInShipping.Contains(c.Id))
            };

            var tempPath = Path.Combine(_appEnvironment.WebRootPath, "temp");

            var maker = new ReportMaker(tempPath);

            return Content(maker.MakeShipping(model));

        }

    }
}