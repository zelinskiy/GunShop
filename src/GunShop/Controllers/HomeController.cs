using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using GunShop.ViewModels.HomeViewModels;
using GunShop.Models;
using Microsoft.EntityFrameworkCore;
using GunShop.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace GunShop.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;
        ILogger _logger;

        public HomeController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            ICommoditiesService commoditiesService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.CommodityBOs = _commoditiesService.GetAllCommodities();
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCommodityType(CommodityTypeViewModel CommodityTypeViewModel)
        {
            _logger.LogWarning($"Added Commiodity {CommodityTypeViewModel.Model}");
            if (ModelState.IsValid)
            {
                var newtype = _context.CommoditiesTypes.Add(new CommodityType()
                {
                    Model = CommodityTypeViewModel.Model,
                    Size = CommodityTypeViewModel.Size,
                    Weight = CommodityTypeViewModel.Weight,
                    ManufacturerId = CommodityTypeViewModel.ManufacturerId
                });
                await _context.SaveChangesAsync();
                for (int i = 0; i < CommodityTypeViewModel.InitialCount; i++)
                {
                    _context.Commodities.Add(new Commodity { CommodityTypeId = newtype.Entity.Id });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Can't add Commiodity {CommodityTypeViewModel.Model}");
                _logger.LogError(ModelState.ValidationState.ToString());
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

        [HttpGet]
        public async Task<IActionResult> AllCommodityTypes()
        {
            return View(_commoditiesService.GetAllCommoditiesTypes());
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }



        public IActionResult Error()
        {
            return View();
        }
    }
}
