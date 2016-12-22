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
using GunShop.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
            _logger = loggerFactory.CreateLogger<HomeController>();
            _categorizationService = categorizationService;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("AllCommodityTypes", "Commodity");
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Admin()
        {
            return View();
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

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult ExecuteSql()
        {
            ViewData["result"] = "";
            return View();

        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult ExecuteSql(string query)
        {
            var forbiddenWords = new string[]
            {
                " UPDATE ",
                " DROP ",
                " ALTER "
            };

            ViewData["query"] = query;
            foreach (var word in forbiddenWords)
            {
                if (query.ToUpper().Contains(word))
                {
                    ViewData["error"] = "Word " + word + " is forbidden";
                    return View();
                }
            }
            

            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            var maker = new SqlMaker(connectionString);
            var result = "";
            if (maker.TryExecuteQuery(query, ref result))
            {
                ViewData["error"] = null;
            }
            else
            {
                ViewData["error"] = result;
                return View();
            }

            ViewData["result"] = result;

            return View();
        }

        public IActionResult SetupInit()
        {
            if(_context.Roles.FirstOrDefault(r=>r.NormalizedName == "ADMIN") == null)
            {
                _context.Roles.Add(new IdentityRole("ADMIN"));
                _context.SaveChanges();
            }
            if (_context.Roles.FirstOrDefault(r => r.NormalizedName == "EMPLOYEE") == null)
            {
                _context.Roles.Add(new IdentityRole("EMPLOYEE"));
                _context.SaveChanges();
            }
            return Content("OK");
            
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
