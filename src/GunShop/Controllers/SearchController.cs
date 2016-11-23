using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using Microsoft.Extensions.Logging;
using GunShop.ViewModels.SearchViewModels;
using GunShop.Models;

namespace GunShop.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext _context;
        ILogger _logger;

        public SearchController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SearchController>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new SearchViewModel()
            {
                Categories = _context.Categories.ToArray(),
                AllCharacteristics = _context.Characteristics.ToArray(),
                Results = _context.CommoditiesTypes.ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel model)
        {
            var parsedSelectedCategories = new List<int>();
            if (model.SelectedCategoriesIds != null)
            {
                parsedSelectedCategories = model.SelectedCategoriesIds
                    .Split(';')
                    .Select(id => int.Parse(id))
                    .ToList();
            }
            var parsedSelectedCharVals = new List<CharacteristicValue>();
            if (model.SelectedCharVals != null)
            {
                parsedSelectedCharVals = model.SelectedCharVals
                    .Split(';')
                    .Select(pair => new CharacteristicValue
                    {
                        CharacteristicId = int.Parse(pair.Split(',').First()),
                        Value = string.Join("", pair.Split(',').Skip(1))
                    })
                    .ToList();                
            }
            

            var commodityTypesIdsInSelectedCategories = _context.CommoditiesTypesInCathegories
                .Where(ctic => parsedSelectedCategories.Contains(ctic.CategoryId))
                .Select(ctic => ctic.CommodityTypeId)
                .ToArray();

            var commodityTypesIdsHavingCharVal = _context.CharacteristicValues
                .Where(cv => parsedSelectedCharVals
                    .Count(scv => scv.Value == cv.Value && scv.CharacteristicId == scv.CharacteristicId) > 0)
                .Select(cv => cv.CommodityTypeId)
                .ToArray();

            var results = _context.CommoditiesTypes.ToList();
            if (!string.IsNullOrEmpty(model.ModelNamePattern))
            {
                results = results
                    .Where(ct => ct.Model.Contains(model.ModelNamePattern))
                    .ToList();
            }
            if (parsedSelectedCategories.Count() > 0)
            {
                results = results
                    .Where(ct => commodityTypesIdsInSelectedCategories.Contains(ct.Id))
                    .ToList();
            }
            if (parsedSelectedCharVals.Count() > 0)
            {
                results = results
                    .Where(ct => commodityTypesIdsHavingCharVal.Contains(ct.Id))
                    .ToList();
            }

            model.Results = results;
            model.Categories = _context.Categories.ToArray();
            model.AllCharacteristics = _context.Characteristics
                .Where(c => parsedSelectedCategories.Contains(c.CategoryId))
                .ToArray();

            return View(model);
        }
    }
}