using GunShop.Data;
using GunShop.Models;
using GunShop.Services.Interfaces;
using GunShop.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services
{
    public class CommoditiesService: ICommoditiesService
    {
        private readonly ApplicationDbContext context;

        public CommoditiesService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public CommodityBO[] GetAll()
        {
            return context.Commodities.Join(
                context.CommoditiesTypes,
                c => c.CommodityTypeId,
                ct => ct.Id,
                (c, ct) => new CommodityBO(c).AddType(ct)
            )
            .Join(
                context.Manufacturers,
                cbo => cbo.ManufacturerId,
                m => m.Id,
                (cbo, m) => cbo.AddManufacturer(m)
            )
            .ToArray();
        }
        
    }
}
