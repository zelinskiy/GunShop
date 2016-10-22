using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GunShop.Models;

namespace GunShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityType> CommoditiesTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CommodityInChart> CommoditiesInCharts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CommodityInChart>().HasKey(cic => new { cic.CommodityId, cic.CustomerId });
        }
    }
}
