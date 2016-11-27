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
                Id = storageId,
                Adress = storage.Adress,
                Coordinates = storage.Coordinates,
                Name = storage.Name,
                Commodities = GetCommoditiesInStorage(storageId),
                StorageBCandidates = _context.Storages
                    .Where(s=>s.Id != storageId)
                    .ToArray()
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Storage(StorageViewModel model)
        {
            var selectedCommoditiesIds = model.SelectedCommoditiesIds
                .Split(';')
                .Select(id => int.Parse(id))
                .ToArray();

            var commodities = _context.Commodities
                .Where(c => selectedCommoditiesIds.Contains(c.Id))
                .ToArray();
                      
            var storageA = _context.Storages.FirstOrDefault(s => s.Id == model.Id);
            if(storageA == null)
            {
                return Content($"Storage A {model.Id} not found");
            }

            var storageB = _context.Storages.FirstOrDefault(s => s.Id == model.StorageBId);
            if (storageB == null)
            {
                return Content($"Storage B {model.StorageBId} not found");
            }

            var newShipping = new Shipping()
            {
                AuthorId = "me",
                StorageAId = storageA.Id,
                StorageBId = storageB.Id,
                Date = DateTime.Now
            };
            _context.Shippings.Add(newShipping);
            _context.SaveChanges();

            foreach (var c in commodities)
            {
                c.StorageId = storageB.Id;
                _context.ShippingRows.Add(new ShippingRow() { CommodityId = c.Id, ShippingId = newShipping.Id });
            }
            _context.UpdateRange(commodities);
            _context.SaveChanges();

            return Content(model.StorageBId.ToString());

        }

        public IActionResult AllShippings()
        {
            var shippings = _context.Shippings.Select(s => new ShippingViewModel()
            {
                ShippingId = s.Id,
                AuthorId = s.AuthorId,
                Date = s.Date,
                StorageA = _context.Storages.First(st => st.Id == s.StorageAId),
                StorageB = _context.Storages.First(st => st.Id == s.StorageBId)
            }).ToArray();

            foreach(var s in shippings)
            {
                //s.StorageA = _context.Storages.First(st => st.Id == s.StorageAId);
                //s.StorageB = _context.Storages.First(st => st.Id == s.StorageBId);
                var commoditiesInShipping = _context.ShippingRows
                    .Where(sr => sr.ShippingId == s.ShippingId)
                    .Select(sr => sr.CommodityId)
                    .ToArray();

                s.Commodities = _commoditiesService.GetAllCommodities()
                    .Where(c => commoditiesInShipping.Contains(c.Id));
            }

            return View(shippings);

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

        [HttpGet]
        public IActionResult UpdateStorage(int storageId)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == storageId);
            if (storage == null)
            {
                return NotFound(storageId.ToString() + " storage not found");
            }

            return View(storage);
        }

        [HttpPost]
        public IActionResult UpdateStorage(Storage storage)
        {
            var oldStorage = _context.Storages.FirstOrDefault(s => s.Id == storage.Id);
            if (oldStorage == null)
            {
                return NotFound(storage.Id + " storage not found");
            }

            var associatedShop = _context.Shops.FirstOrDefault(s => s.StorageId== storage.Id);

            if (associatedShop == null)
            {
                oldStorage.Coordinates = storage.Coordinates;
                oldStorage.Adress = storage.Adress;
            }

            oldStorage.Name = storage.Name;

            

            _context.Storages.Update(oldStorage);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateShop(int shopId)
        {
            var shop = _context.Shops.FirstOrDefault(s => s.Id == shopId);
            if (shop == null)
            {
                return NotFound(shopId + " shop not found");
            }



            return View(shop);
        }

        [HttpPost]
        public IActionResult UpdateShop(Shop shop)
        {
            var oldShop = _context.Shops.FirstOrDefault(s => s.Id == shop.Id);
            if (oldShop == null)
            {
                return NotFound(shop.Id + " storage not found");
            }

            oldShop.Adress = shop.Adress;
            oldShop.Name = shop.Name;
            oldShop.Coordinates = shop.Coordinates;


            var associatedStorage = _context.Storages.FirstOrDefault(s => s.Id == shop.StorageId);

            if (associatedStorage != null)
            {
                associatedStorage.Adress = shop.Adress;
                associatedStorage.Coordinates = shop.Coordinates;
                _context.Storages.Update(associatedStorage);
            }

            
            _context.Shops.Update(oldShop);
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