using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using GunShop.ViewModels.HomeViewModels;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using GunShop.Services;
using System.Collections.Generic;

namespace GunShop.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;
        CategorizationService _categorizationService;
        ILogger _logger;

        public HomeController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            IChartService chartService,
            ICommoditiesService commoditiesService,
            CategorizationService categorizationService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _categorizationService = categorizationService;
        }


        public IActionResult Index(int? categoryId)
        {
            var model = new IndexViewModel();
            model.CommodityBOs = _commoditiesService.GetAllCommodities();

            
            return View(model);
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
