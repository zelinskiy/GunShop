﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GunShop.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GunShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityType> CommoditiesTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<CommodityInChart> CommoditiesInCharts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CharacteristicValue> CharacteristicValues { get; set; }
        public DbSet<CommodityTypeInCathegory> CommoditiesTypesInCathegories { get; set; }

        public DbSet<Storage> Storages { get; set; }
        public DbSet<Shop> Shops { get; set; }

        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingRow> ShippingRows { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<CommodityInChart>()
                .HasKey(cic => new { cic.CommodityId, cic.CustomerId });
            builder.Entity<CommodityTypeInCathegory>()
                .HasKey(cic => new { cic.CommodityTypeId, CathegoryId = cic.CategoryId });
            builder.Entity<CharacteristicValue>()
                .HasKey(cv => new { cv.CommodityTypeId, cv.CharacteristicId });
            builder.Entity<ShippingRow>()
                .HasKey(sr => new { sr.CommodityId, sr.ShippingId });

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }

        public DbSet<Storage> Storage { get; set; }
    }
}
