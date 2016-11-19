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

        public CategoryController(
            ApplicationDbContext context,
            ILoggerFactory loggerFactory,
            CategorizationService categorizationService)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _categorizationService = categorizationService;
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
    }
}