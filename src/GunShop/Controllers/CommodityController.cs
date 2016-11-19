using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Services;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GunShop.Controllers
{
    public class CommodityController : Controller
    {
        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;
        ILogger _logger;
        IChartService _chartService;
        CategorizationService _categorizationService;

        public CommodityController(
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


        [HttpPost]
        public async Task<IActionResult> AddCommodityType(CommodityTypeViewModel CommodityTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _commoditiesService.AddCommodityType(CommodityTypeViewModel);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.LogError($"Can't add Commiodity {CommodityTypeViewModel.Model}");
                return View(CommodityTypeViewModel);
            }

        }

        [HttpGet]
        public IActionResult AddCommodityType()
        {
            var model = new CommodityTypeViewModel()
            {
                ManufacturersPreviews = _context
                    .Manufacturers
                    .ToList()
                    .Select(m => new ManufacturerPreview(m))
                    .ToArray(),
                ModelsPreviews = _context.CommoditiesTypes.Select(ct => ct.Model).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddManufacturer(ManufacturerViewModel ManufacturerViewModel)
        {
            if (ModelState.IsValid)
            {
                await _commoditiesService.AddManufacturer(ManufacturerViewModel);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.LogError($"Can't add Manufacturer { ManufacturerViewModel.Name }");
                return View(ManufacturerViewModel);
            }

        }

        [HttpGet]
        public IActionResult AddManufacturer()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllCommodityTypes()
        {
            return View(_commoditiesService.GetAllCommoditiesTypes());
        }
    }
}