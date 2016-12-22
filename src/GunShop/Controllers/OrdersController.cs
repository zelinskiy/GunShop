using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GunShop.Data;
using GunShop.Models;
using GunShop.ViewModels.OrdersViewModels;
using GunShop.Services;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext _context;
        ICommoditiesService _commoditiesService;

        public OrdersController (
            ApplicationDbContext context,
            ICommoditiesService commoditiesService)
        {
            _context = context;
            _commoditiesService = commoditiesService;
        }

        private int CustomerId
        {
            get
            {
                if (!Request.Cookies.ContainsKey("USERID"))
                {
                    throw new Exception();
                }
                else
                {
                    return int.Parse(Request.Cookies["USERID"]);
                }
            }
        }

        private IEnumerable<OrderViewModel> getOrders(Func<Order,bool> restrict)
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Where(restrict)
                .ToArray()
                .Select(o => new OrderViewModel(o))
                .ToArray();
            foreach (var o in orders)
            {
                o.Commodities = _commoditiesService
                    .GetAllCommodities()
                    .Where(cbo => cbo.OrderId == o.Id)
                    .ToArray();
            }
            return orders;
        }

        public IActionResult My()
        {
            return View("Views/Orders/Index.cshtml", getOrders(o=>o.CustomerId == CustomerId));
        }

        public IActionResult All()
        {
            return View("Views/Orders/Index.cshtml", getOrders(o => true));
        }

        public IActionResult MakeOrder()
        {
            var commoditiesInChart = _context.CommoditiesInCharts
                .Where(cic => cic.CustomerId == CustomerId)
                .ToArray();

            _context.RemoveRange(commoditiesInChart);
            _context.SaveChanges();

            var newOrder = new Order()
            {
                DateTime = DateTime.Now,
                CustomerId = CustomerId
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            var commoditiesIds = commoditiesInChart
                .Select(cic=>cic.CommodityId)
                .ToArray();            

            var commodities = _context.Commodities
                .Where(c => commoditiesIds.Contains(c.Id))
                .ToArray();

            foreach(var c in commodities)
            {
                c.OrderId = newOrder.Id;
            }

            _context.Commodities.UpdateRange(commodities);
            _context.SaveChanges();

            return RedirectToAction("My", "Orders");
        }


        public IActionResult RemoveOrder(int orderId)
        {
            var ordered = _context.Commodities
                .Where(c => c.OrderId == orderId)
                .ToArray();
            foreach(var ord in ordered)
            {
                ord.OrderId = null;
            }
            _context.UpdateRange(ordered);
            var order = _context.Orders
                .FirstOrDefault(o => o.Id == orderId);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("My", "Orders");
        }

        public IActionResult RemoveFromOrder(int commodityId)
        {            
            var commodity = _context.Commodities
                .FirstOrDefault(c => c.Id == commodityId);
            var order = _context.Orders
                .FirstOrDefault(o => o.Id == commodity.OrderId);
            if(_context.Commodities.Count(c=>c.OrderId == order.Id) == 1)
            {
                _context.Orders.Remove(order);
            }
            commodity.OrderId = null;
            _context.Update(commodity);
            _context.SaveChanges();
            return RedirectToAction("My", "Orders");
        }

        

    }
}