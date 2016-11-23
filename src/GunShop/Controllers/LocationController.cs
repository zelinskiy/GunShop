using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using Microsoft.Extensions.Logging;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.CommodityViewModels;
using GunShop.ViewModels.LocationViewModels;
using GunShop.Models;

namespace GunShop.Controllers
{
    public class LocationController : Controller
    {

        ApplicationDbContext _context;
        ILogger _logger;
        ICommoditiesService _commoditiesService;

        public LocationController(
            ApplicationDbContext context,
            ICommoditiesService commoditiesService,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<LocationController>();
            _commoditiesService = commoditiesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Storages = _context.Storages
                    .ToArray()
                    .Select(s => new StorageBO(s, GetCommoditiesInStorage(s.Id)))
                    .ToArray(),
                Shops = _context.Shops.ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            model.Storages = _context.Storages
                    .ToArray()
                    .Select(s => new StorageBO(s, GetCommoditiesInStorage(s.Id)))
                    .ToArray();
            model.Shops = _context.Shops.ToArray();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newStorage = new Storage()
            {
                Adress = model.Adress,
                Coordinates = model.Coordinates
            };

            if (model.IsShop)
            {
                newStorage.Name = model.Name + "_Storage";
            }
            else
            {
                newStorage.Name = model.Name;
            }

            _context.Storages.Add(newStorage);
            _context.SaveChanges();
            if (model.IsShop)
            {
                var newShop = new Shop()
                {
                    Adress = model.Adress,
                    Coordinates = model.Coordinates,
                    Name = model.Name,
                    StorageId = newStorage.Id
                };

                _context.Shops.Add(newShop);
                _context.SaveChanges();
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Storage(int storageId)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == storageId);
            if(storage == null)
            {
                return NotFound($"Storage {storageId} not found!");
            }
            var model = new StorageViewModel()
            {
                StorageBO = new StorageBO(storage, GetCommoditiesInStorage(storage.Id))
            };
            return View(model);
        }
        
        [HttpGet]
        public IActionResult AddShipping(IEnumerable<int> commoditiesIds, int storageAId)
        {
            if(commoditiesIds == null || commoditiesIds.Count() == 0)
            {
                return NotFound("Empty ids list");
            }

            var commodities = _commoditiesService.GetAllCommodities()
                .Where(c => commoditiesIds.Contains(c.Id))
                .ToArray();

            var storageA = _context.Storages.FirstOrDefault(s => s.Id == storageAId);
            if(storageA == null)
            {
                return NotFound($"Storage {storageAId} not found");
            }

            var model = new ShippingViewModel()
            {
                Commodities = commodities,
                StorageAId = storageAId,
                StorageA = storageA,
                StorageBCandidates = _context.Storages
                    .Where(s => s.Id != storageAId).ToArray()
            };

            return View(model);

        }

        public IActionResult DeleteShop(int shopId)
        {
            var shop = _context.Shops.FirstOrDefault(s => s.Id == shopId);
            if (shop == null)
            {
                return NotFound(shopId.ToString() + " shop not found");
            }
            var storage = _context.Storages.FirstOrDefault(s => s.Id == shop.StorageId);
            if(storage != null)
            {
                _context.Storages.Remove(storage);
            }            
            _context.Shops.Remove(shop);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStorage(int storageId)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == storageId);
            if (storage == null)
            {
                return NotFound(storageId.ToString() + " storage not found");
            }
            _context.Storages.Remove(storage);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        private IEnumerable<CommodityBO> GetCommoditiesInStorage(int storageId)
        {
            return _commoditiesService.GetAllCommodities()
                .Where(c => c.StorageId == storageId)
                .ToArray();

        }
    }
}