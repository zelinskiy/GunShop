using GunShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.ViewModels.SearchViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Characteristic> AllCharacteristics{ get; set; }

        public string ModelNamePattern { get; set; }
        public string DescriptionPattern { get; set; }        
        public string SelectedCategoriesIds { get; set; }
        public string SelectedCharVals { get; set; }

        public IEnumerable<CommodityType> Results { get; set; }
    }
}
