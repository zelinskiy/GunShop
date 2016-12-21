using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Models;
using GunShop.Services;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.CommodityViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

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

        public Storage RootStorage
        {
            get
            {
                var storage = _context.Storages.FirstOrDefault(s => s.Name == "ROOT");
                if (storage == null)
                {
                    storage = new Storage
                    {
                        Adress = "Nowhere",
                        Coordinates = "0;0;0",
                        Name = "ROOT"
                    };

                    _context.Storages.Add(storage);
                    _context.SaveChanges();
                }
                return storage;
            }
        }

        [HttpGet]
        public IActionResult AddCommodityType()
        {
            var model = new CommodityTypeViewModel()
            {
                ManufacturersPreviews = _context
                    .Manufacturers
                    .ToArray()
                    .Select(m => new ManufacturerPreview(m)),
                StoragesPreviews = _context
                    .Storages
                    .ToArray()
                    .Select(s => new StoragePreview(s)),
                ModelsPreviews = _context.CommoditiesTypes.Select(ct => ct.Model).ToArray()
            };
            return View(model);
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
        public IActionResult AddManufacturer()
        {
            return View();
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
        public IActionResult AllCommodityTypes(int? categoryId)
        {
            var model = new AllCommodityTypesViewModel();
            if (categoryId != null)
            {
                model.SubCategories = _categorizationService.GetSubCategories(categoryId.Value);
                var inCategory = _categorizationService.CommoditiesTypesIdsInCategory(categoryId.Value);
                model.CommoditiesTypes = _commoditiesService
                    .GetAllCommoditiesTypes()
                    .Where(ctbo => inCategory.Contains(ctbo.Id))
                    .ToArray();
            }
            else
            {
                var rootCategory = _context.Categories.FirstOrDefault(c => c.MasterCategoryId == null);
                model.SubCategories = _categorizationService.GetSubCategories(rootCategory.Id);
                model.CommoditiesTypes = _commoditiesService.GetAllCommoditiesTypes();
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int commodityTypeId)
        {
            var commodityTypeBO = _commoditiesService
                .GetAllCommoditiesTypes()
                .FirstOrDefault(ctbo => ctbo.Id == commodityTypeId);

            if(commodityTypeBO == null)
            {
                return NotFound($"commodity type {commodityTypeId} not found");
            }
            commodityTypeBO.Categories = _context.CommoditiesTypesInCathegories
                .Where(ctic => ctic.CommodityTypeId == commodityTypeId)
                .Join(
                    _context.Categories,
                    ctic => ctic.CategoryId,
                    cat => cat.Id,
                    (ctic, cat) => cat
                ).ToArray();

            return View(commodityTypeBO);
        }

        [HttpPost]
        public IActionResult AddCommoditiesOfType(int commodityTypeId, int count)
        {
            var commodityType = _context.CommoditiesTypes
                .FirstOrDefault(ct => ct.Id == commodityTypeId);

            if (commodityType == null)
            {
                return Content("Not Found commodity type " + commodityTypeId);
            }
            for (int i = 0; i < count; i++)
            {
                _context.Commodities.Add(new Commodity
                {
                    CommodityTypeId = commodityTypeId,
                    StorageId = RootStorage.Id,
                    OrderId = null
                });
            }
           
            _context.SaveChanges();
            return RedirectToAction("Storage", "Location", new {storageId = RootStorage.Id});

        }

    }
}