﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Besho.DAL.Dats.Migrations
{
    /// <inheritdoc />
    public partial class upd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Carts");
        }
    }
}
