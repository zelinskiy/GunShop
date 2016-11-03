using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GunShop.Data.Migrations
{
    public partial class fixedInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CathegoryId",
                table: "Characteristics");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Characteristics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MasterCategoryId",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Characteristics");

            migrationBuilder.AddColumn<int>(
                name: "CathegoryId",
                table: "Characteristics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MasterCategoryId",
                table: "Categories",
                nullable: false);
        }
    }
}
