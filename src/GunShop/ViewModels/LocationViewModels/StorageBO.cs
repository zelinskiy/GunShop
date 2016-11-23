using GunShop.Models;
using GunShop.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.LocationViewModels
{
    public class StorageBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Coordinates { get; set; }

        public IEnumerable<CommodityBO> StoredCommodities { get; set; }

        public StorageBO(Storage storage, IEnumerable<CommodityBO> commodities)
        {
            Id = storage.Id;
            Name = storage.Name;
            Adress = storage.Adress;
            Coordinates = storage.Coordinates;
            StoredCommodities = commodities;
        }

        public StorageBO() { }
    }
}
