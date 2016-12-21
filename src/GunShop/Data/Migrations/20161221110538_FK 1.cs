using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GunShop.Data.Migrations
{
    public partial class FK1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommoditiesInStorages");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesInCharts_CommodityId",
                table: "CommoditiesInCharts",
                column: "CommodityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommoditiesInCharts_CustomerId",
                table: "CommoditiesInCharts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Commodities_CommodityTypeId",
                table: "Commodities",
                column: "CommodityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commodities_OrderId",
                table: "Commodities",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Commodities_StorageId",
                table: "Commodities",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CharacteristicId",
                table: "CharacteristicValues",
                column: "CharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CommodityTypeId",
                table: "CharacteristicValues",
                column: "CommodityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CategoryId",
                table: "Characteristics",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_MasterCategoryId",
                table: "Categories",
                column: "MasterCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_MasterCategoryId",
                table: "Categories",
                column: "MasterCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Commodities_Orders_OrderId",
                table: "Commodities",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_MasterCategoryId",
                table: "Categories");

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
                name: "FK_Commodities_Orders_OrderId",
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

            migrationBuilder.DropIndex(
                name: "IX_CommoditiesInCharts_CommodityId",
                table: "CommoditiesInCharts");

            migrationBuilder.DropIndex(
                name: "IX_CommoditiesInCharts_CustomerId",
                table: "CommoditiesInCharts");

            migrationBuilder.DropIndex(
                name: "IX_Commodities_CommodityTypeId",
                table: "Commodities");

            migrationBuilder.DropIndex(
                name: "IX_Commodities_OrderId",
                table: "Commodities");

            migrationBuilder.DropIndex(
                name: "IX_Commodities_StorageId",
                table: "Commodities");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValues_CharacteristicId",
                table: "CharacteristicValues");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValues_CommodityTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_CategoryId",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_Categories_MasterCategoryId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CommoditiesInStorages",
                columns: table => new
                {
                    CommodityId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StoredAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommoditiesInStorages", x => new { x.CommodityId, x.StorageId });
                });
        }
    }
}
