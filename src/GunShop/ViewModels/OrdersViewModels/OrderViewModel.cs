using GunShop.Models;
using GunShop.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.OrdersViewModels
{
    public class OrderViewModel:Order
    {
        public IEnumerable<CommodityBO> Commodities { get; set; }

        public OrderViewModel(Order baseOrder)
        {
            this.Id = baseOrder.Id;
            this.DateTime = baseOrder.DateTime;
            this.CustomerId = baseOrder.CustomerId;
            this.Customer = baseOrder.Customer;
        }
    }
}
