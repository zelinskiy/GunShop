using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GunShop.Data.Migrations
{
    public partial class FK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Categories_CategoryId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_Characteristics_CharacteristicId",
                table: "CharacteristicValues");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_CommoditiesTypes_CommodityTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Commodities_CommoditiesTypes_CommodityTypeId",
                table: "Commodities");

            migrationBuilder.DropForeignKey(
                name: "FK_Commodities_Storage_StorageId",
                table: "Commodities");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesInCharts_Commodities_CommodityId",
                table: "CommoditiesInCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesInCharts_Customers_CustomerId",
                table: "CommoditiesInCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_StorageId",
                table: "Shops",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingRows_CommodityId",
                table: "ShippingRows",
                column: "CommodityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingRows_ShippingId",
                table: "ShippingRows",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_StorageAId",
                table: "Shippings",
                column: "StorageAId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_StorageBId",
                table: "Shippings",
                column: "StorageBId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesTypesInCathegories_CategoryId",
                table: "CommoditiesTypesInCathegories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesTypesInCathegories_CommodityTypeId",
                table: "CommoditiesTypesInCathegories",
                column: "CommodityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Categories_CategoryId",
                table: "Characteristics",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_Characteristics_CharacteristicId",
                table: "CharacteristicValues",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_CommoditiesTypes_CommodityTypeId",
                table: "CharacteristicValues",
                column: "CommodityTypeId",
                principalTable: "CommoditiesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commodities_CommoditiesTypes_CommodityTypeId",
                table: "Commodities",
                column: "CommodityTypeId",
                principalTable: "CommoditiesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commodities_Storage_StorageId",
                table: "Commodities",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesInCharts_Commodities_CommodityId",
                table: "CommoditiesInCharts",
                column: "CommodityId",
                principalTable: "Commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesInCharts_Customers_CustomerId",
                table: "CommoditiesInCharts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesTypesInCathegories_Categories_CategoryId",
                table: "CommoditiesTypesInCathegories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesTypesInCathegories_CommoditiesTypes_CommodityTypeId",
                table: "CommoditiesTypesInCathegories",
                column: "CommodityTypeId",
                principalTable: "CommoditiesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Storage_StorageAId",
                table: "Shippings",
                column: "StorageAId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Storage_StorageBId",
                table: "Shippings",
                column: "StorageBId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingRows_Commodities_CommodityId",
                table: "ShippingRows",
                column: "CommodityId",
                principalTable: "Commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingRows_Shippings_ShippingId",
                table: "ShippingRows",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Storage_StorageId",
                table: "Shops",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Categories_CategoryId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_Characteristics_CharacteristicId",
                table: "CharacteristicValues");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_CommoditiesTypes_CommodityTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Commodities_CommoditiesTypes_CommodityTypeId",
                table: "Commodities");

            migrationBuilder.DropForeignKey(
                name: "FK_Commodities_Storage_StorageId",
                table: "Commodities");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesInCharts_Commodities_CommodityId",
                table: "CommoditiesInCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesInCharts_Customers_CustomerId",
                table: "CommoditiesInCharts");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesTypesInCathegories_Categories_CategoryId",
                table: "CommoditiesTypesInCathegories");

            migrationBuilder.DropForeignKey(
                name: "FK_CommoditiesTypesInCathegories_CommoditiesTypes_CommodityTypeId",
                table: "CommoditiesTypesInCathegories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Storage_StorageAId",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Storage_StorageBId",
                table: "Shippings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingRows_Commodities_CommodityId",
                table: "ShippingRows");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingRows_Shippings_ShippingId",
                table: "ShippingRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Storage_StorageId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Shops_StorageId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_ShippingRows_CommodityId",
                table: "ShippingRows");

            migrationBuilder.DropIndex(
                name: "IX_ShippingRows_ShippingId",
                table: "ShippingRows");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_StorageAId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_StorageBId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CommoditiesTypesInCathegories_CategoryId",
                table: "CommoditiesTypesInCathegories");

            migrationBuilder.DropIndex(
                name: "IX_CommoditiesTypesInCathegories_CommodityTypeId",
                table: "CommoditiesTypesInCathegories");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Categories_CategoryId",
                table: "Characteristics",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_Characteristics_CharacteristicId",
                table: "CharacteristicValues",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_CommoditiesTypes_CommodityTypeId",
                table: "CharacteristicValues",
                column: "CommodityTypeId",
                principalTable: "CommoditiesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commodities_CommoditiesTypes_CommodityTypeId",
                table: "Commodities",
                column: "CommodityTypeId",
                principalTable: "CommoditiesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commodities_Storage_StorageId",
                table: "Commodities",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesInCharts_Commodities_CommodityId",
                table: "CommoditiesInCharts",
                column: "CommodityId",
                principalTable: "Commodities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommoditiesInCharts_Customers_CustomerId",
                table: "CommoditiesInCharts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
