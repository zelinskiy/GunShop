using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Models;
using GunShop.Services;
using GunShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GunShop.Controllers
{
    public class ChartController : Controller
    {
        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;
        ILogger _logger;
        IChartService _chartService;
        CategorizationService _categorizationService;

        public ChartController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            IChartService chartService,
            ICommoditiesService commoditiesService,
            CategorizationService categorizationService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _chartService = chartService;
            _categorizationService = categorizationService;
        }

        private int CustomerId
        {
            get
            {
                if (!Request.Cookies.ContainsKey("USERID"))
                {
                    var tempUser = _context.Customers.Add(new Customer { IsTemp = true });
                    _context.SaveChanges();
                    Response.Cookies.Append("USERID", tempUser.Entity.Id.ToString());
                    return tempUser.Entity.Id;
                }
                else
                {
                    return int.Parse(Request.Cookies["USERID"]);
                }

            }
        }





        [HttpPost]
        public IActionResult AddCommodityToChart(int commodityTypeId)
        {
            if (_commoditiesService.HasAvailableCommodity(commodityTypeId))
            {
                _chartService.AddToChart(CustomerId, commodityTypeId);
            }
            else
            {
                _logger.LogWarning("Trying to add commodity whish is not available");
            }

            return RedirectToAction("MyChart");
        }

        [HttpGet]
        public IActionResult MyChart()
        {
            return View("/Views/Chart/Chart.cshtml", _chartService.OnChart(CustomerId));
        }

        [HttpPost]
        public IActionResult RemoveCommodityFromChart(int commodityId)
        {
            _chartService.RemoveFromChart(CustomerId, commodityId);
            return RedirectToAction("MyChart");
        }

        [HttpGet]
        public IActionResult ClearAllAnonymousCharts()
        {
            _chartService.ClearAllAnonymousCharts();
            return RedirectToAction("Index", "Home");
        }
    }
}