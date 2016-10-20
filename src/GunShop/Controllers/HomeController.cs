using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using GunShop.ViewModels.HomeViewModels;
using GunShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.Commodities = _context
                .Commodities                
                .ToList();
            var x = _context.Commodities.Join(
                _context.CommoditiesTypes,
                c => c.CommodityTypeId,
                ct => ct.Id,
                (c, ct) => new Commodity()
                    {
                        Id = c.Id,
                        CommodityTypeId = c.CommodityTypeId,
                        CustomerId = c.CustomerId,
                        Weight = ct.Weight,
                        Size = ct.Size,
                        Model = ct.Model
                    });
            model.Commodities = x.ToArray();
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
