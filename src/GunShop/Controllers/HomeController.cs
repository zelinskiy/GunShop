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
using Microsoft.AspNetCore.Identity;

namespace GunShop.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;
        CategorizationService _categorizationService;
        UserManager<ApplicationUser> _userManager;
        ILogger _logger;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory,
            IChartService chartService,
            ICommoditiesService commoditiesService,
            CategorizationService categorizationService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _categorizationService = categorizationService;
            _userManager = userManager;
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
        
        [Authorize]
        public async Task<IActionResult> SetupInit()
        {
            if(_context.Roles.FirstOrDefault(r=>r.NormalizedName == "ADMIN") == null)
            {
                _context.Roles.Add(new IdentityRole("ADMIN") { NormalizedName = "ADMIN" });
                await _userManager.AddToRoleAsync(_userManager.Users.First(), "ADMIN");
                _context.SaveChanges();
            }
            if (_context.Roles.FirstOrDefault(r => r.NormalizedName == "EMPLOYEE") == null)
            {
                _context.Roles.Add(new IdentityRole("EMPLOYEE") { NormalizedName = "EMPLOYEE" });
                _context.SaveChanges();
            }
            if (!User.IsInRole("ADMIN"))
            {
                await _userManager.AddToRoleAsync(
                    await _userManager.FindByNameAsync(User.Identity.Name), "ADMIN");
            }
            var rootStorage = _context.Storages.FirstOrDefault(s => s.Name == "ROOT");
            if (rootStorage == null)
            {
                _context.Storages.Add(new Storage()
                {
                    Name = "ROOT",
                    Adress = "Nowhere",
                    Coordinates = "0,0,0"
                });
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
