using GunShop.Models;
using GunShop.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.LocationViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Coordinates { get; set; }
        public IEnumerable<Storage> StorageBCandidates { get; set; }
        public IEnumerable<CommodityBO> Commodities { get; set; }

        [Required]
        [RegularExpression(@"^[0-9;]{1,}$")]
        public string SelectedCommoditiesIds { get; set; }        
        public int StorageBId { get; set; }
    }
}
