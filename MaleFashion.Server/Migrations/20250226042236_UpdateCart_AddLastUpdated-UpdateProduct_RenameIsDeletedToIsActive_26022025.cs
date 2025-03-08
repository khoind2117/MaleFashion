using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaleFashion.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCart_AddLastUpdatedUpdateProduct_RenameIsDeletedToIsActive_26022025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Product",
                newName: "IsActive");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Cart",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Product",
                newName: "IsDeleted");
        }
    }
}
