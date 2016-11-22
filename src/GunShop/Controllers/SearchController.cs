using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using Microsoft.Extensions.Logging;
using GunShop.ViewModels.HomeViewModels;

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
            return View();
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel model)
        {
            return View();
        }
    }
}