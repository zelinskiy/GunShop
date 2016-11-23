using GunShop.Models;
using GunShop.Models.Interfaces;
using Microsoft.AspNetCore.Razor.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace GunShop.ViewModels.CommodityViewModels
{

    public class ManufacturerPreview
    {
        public int Id { get; set; }
        public string Preview { get; set; }

        public ManufacturerPreview() { }

        public ManufacturerPreview(Manufacturer m)
        {
            Id = m.Id;
            Preview = $"{m.Name}({m.Country})";
        }
    }

    public class CommodityTypeViewModel : ICommodityType
    {
        public int Id { get; set; }

        public IEnumerable<string> ModelsPreviews { get; set; }
        public IEnumerable<ManufacturerPreview> ManufacturersPreviews { get; set; }

        [Range(0, 1000000000, ErrorMessage ="Incorrect Value")]
        public int InitialCount { get; set; }
        
        [Required(ErrorMessage ="Can't be empty")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        public string Size { get; set; }

        [Range(0, 1000000000, ErrorMessage = "Incorrect value")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        public int ManufacturerId { get; set; }

    }
}
