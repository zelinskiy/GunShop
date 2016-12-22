using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GunShop.Data;

namespace GunShop.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161222153458_Prices")]
    partial class Prices
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GunShop.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GunShop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MasterCategoryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MasterCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("GunShop.Models.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvailableValues");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("GunShop.Models.CharacteristicValue", b =>
                {
                    b.Property<int>("CommodityTypeId");

                    b.Property<int>("CharacteristicId");

                    b.Property<string>("Value");

                    b.HasKey("CommodityTypeId", "CharacteristicId");

                    b.HasIndex("CharacteristicId");

                    b.HasIndex("CommodityTypeId");

                    b.ToTable("CharacteristicValues");
                });

            modelBuilder.Entity("GunShop.Models.Commodity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommodityTypeId");

                    b.Property<int?>("OrderId");

                    b.Property<int>("StorageId");

                    b.HasKey("Id");

                    b.HasIndex("CommodityTypeId");

                    b.HasIndex("OrderId");

                    b.HasIndex("StorageId");

                    b.ToTable("Commodities");
                });

            modelBuilder.Entity("GunShop.Models.CommodityInChart", b =>
                {
                    b.Property<int>("CommodityId");

                    b.Property<int>("CustomerId");

                    b.HasKey("CommodityId", "CustomerId");

                    b.HasIndex("CommodityId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CommoditiesInCharts");
                });

            modelBuilder.Entity("GunShop.Models.CommodityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ManufacturerId");

                    b.Property<string>("Model");

                    b.Property<int>("Price");

                    b.Property<string>("Size");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.ToTable("CommoditiesTypes");
                });

            modelBuilder.Entity("GunShop.Models.CommodityTypeInCathegory", b =>
                {
                    b.Property<int>("CommodityTypeId");

                    b.Property<int>("CategoryId");

                    b.HasKey("CommodityTypeId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CommodityTypeId");

                    b.ToTable("CommoditiesTypesInCathegories");
                });

            modelBuilder.Entity("GunShop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Email");

                    b.Property<bool>("IsTemp");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("GunShop.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("GunShop.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GunShop.Models.Shipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("StorageAId");

                    b.Property<int>("StorageBId");

                    b.HasKey("Id");

                    b.HasIndex("StorageAId");

                    b.HasIndex("StorageBId");

                    b.ToTable("Shippings");
                });

            modelBuilder.Entity("GunShop.Models.ShippingRow", b =>
                {
                    b.Property<int>("CommodityId");

                    b.Property<int>("ShippingId");

                    b.HasKey("CommodityId", "ShippingId");

                    b.HasIndex("CommodityId");

                    b.HasIndex("ShippingId");

                    b.ToTable("ShippingRows");
                });

            modelBuilder.Entity("GunShop.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("Coordinates");

                    b.Property<string>("Name");

                    b.Property<int>("StorageId");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("GunShop.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("Coordinates");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Storage");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GunShop.Models.Category", b =>
                {
                    b.HasOne("GunShop.Models.Category")
                        .WithMany()
                        .HasForeignKey("MasterCategoryId");
                });

            modelBuilder.Entity("GunShop.Models.Characteristic", b =>
                {
                    b.HasOne("GunShop.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("GunShop.Models.CharacteristicValue", b =>
                {
                    b.HasOne("GunShop.Models.Characteristic")
                        .WithMany()
                        .HasForeignKey("CharacteristicId");

                    b.HasOne("GunShop.Models.CommodityType")
                        .WithMany()
                        .HasForeignKey("CommodityTypeId");
                });

            modelBuilder.Entity("GunShop.Models.Commodity", b =>
                {
                    b.HasOne("GunShop.Models.CommodityType")
                        .WithMany()
                        .HasForeignKey("CommodityTypeId");

                    b.HasOne("GunShop.Models.Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("GunShop.Models.Storage")
                        .WithMany()
                        .HasForeignKey("StorageId");
                });

            modelBuilder.Entity("GunShop.Models.CommodityInChart", b =>
                {
                    b.HasOne("GunShop.Models.Commodity")
                        .WithMany()
                        .HasForeignKey("CommodityId");

                    b.HasOne("GunShop.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("GunShop.Models.CommodityTypeInCathegory", b =>
                {
                    b.HasOne("GunShop.Models.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("GunShop.Models.CommodityType")
                        .WithMany()
                        .HasForeignKey("CommodityTypeId");
                });

            modelBuilder.Entity("GunShop.Models.Order", b =>
                {
                    b.HasOne("GunShop.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("GunShop.Models.Shipping", b =>
                {
                    b.HasOne("GunShop.Models.Storage")
                        .WithMany()
                        .HasForeignKey("StorageAId");

                    b.HasOne("GunShop.Models.Storage")
                        .WithMany()
                        .HasForeignKey("StorageBId");
                });

            modelBuilder.Entity("GunShop.Models.ShippingRow", b =>
                {
                    b.HasOne("GunShop.Models.Commodity")
                        .WithMany()
                        .HasForeignKey("CommodityId");

                    b.HasOne("GunShop.Models.Shipping")
                        .WithMany()
                        .HasForeignKey("ShippingId");
                });

            modelBuilder.Entity("GunShop.Models.Shop", b =>
                {
                    b.HasOne("GunShop.Models.Storage")
                        .WithMany()
                        .HasForeignKey("StorageId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GunShop.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GunShop.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("GunShop.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
