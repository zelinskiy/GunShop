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
        IChartService _chartService;

        public HomeController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            IChartService chartService,
            ICommoditiesService commoditiesService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _chartService = chartService;
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

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.CommodityBOs = _commoditiesService.GetAllCommodities();
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCommodityType(CommodityTypeViewModel CommodityTypeViewModel)
        {            
            if (ModelState.IsValid)
            {
                await _commoditiesService.AddCommodityType(CommodityTypeViewModel);
                return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<IActionResult> AllCommodityTypes()
        {
            return View(_commoditiesService.GetAllCommoditiesTypes());
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
            return View("/Views/Home/Chart.cshtml", _chartService.OnChart(CustomerId));
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
            return RedirectToAction("Index");
        }

        //******************************************************************************

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
