using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Models;
using GunShop.Services;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GunShop.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _context;
        ILogger _logger;
        CategorizationService _categorizationService;
        ICommoditiesService _commoditiesService;

        public CategoryController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            ICommoditiesService commoditiesService,
            CategorizationService categorizationService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _categorizationService = categorizationService;
            _commoditiesService = commoditiesService;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new CategoryViewModel();
            model.AllCategories = _context.Categories.ToArray();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _categorizationService.AddCategory(model, model.Characteristics);

                return Content("OK");
            }
            else
            {
                _logger.LogError($"Can't add Category {model.Name}");
                return Content("FAIL");
            }
        }


        [HttpGet]
        public IActionResult SelectCategoryToAdd(int commodityid)
        {
            var model = new SelectCategoryToAddViewModel();
            model.CommodityTypeId = commodityid;
            model.Categories = _context.Categories.ToArray();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddCommodityTypeToCategory(int commodityid, int categoryid)
        {
            var model = new AddCommodityTypeToCategoryViewModel();
            var ct = _context.CommoditiesTypes
                .FirstOrDefault(x => x.Id == commodityid);
            var cat = _context.Categories
                .FirstOrDefault(x => x.Id == categoryid);
            model.CommodityTypeId = ct.Id;
            model.CategoryId = cat.Id;
            model.CategoryName = cat.Name;
            model.CommodityTypeModel = ct.Model;
            model.Characteristics = _context.Characteristics
                .Where(c => c.CategoryId == cat.Id).ToArray();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCommodityTypeToCategory(AddCommodityTypeToCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ct = _context.CommoditiesTypes
                    .FirstOrDefault(x => x.Id == model.CommodityTypeId);
                var cat = _context.Categories
                    .FirstOrDefault(x => x.Id == model.CategoryId);
                if (ct == null || cat == null)
                {
                    return Content("FAIL");
                }
                _categorizationService.AddCommodityTypeToCategory(ct,cat,model.CharacteristicsValues);
                return Content("OK");
            }
            else
            {
                _logger.LogError($"Can't add CommodityType {model.CommodityTypeModel}({model.CommodityTypeId}) " +
                                 $"to Category {model.CategoryName}({model.CategoryName})");
                return Content("FAIL");
            }
        }
        


    }
}