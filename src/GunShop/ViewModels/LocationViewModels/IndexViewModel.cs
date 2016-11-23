using GunShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.LocationViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<StorageBO> Storages { get; set; }
        public IEnumerable<Shop> Shops { get; set; }

        public bool IsShop { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Adress { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,}.?[0-9]{0,},[0-9]{1,}.?[0-9]{0,},[0-9]{1,}.?[0-9]{0,}$")]
        public string Coordinates { get; set; }
    }
}
