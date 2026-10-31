using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addOffersIds4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Shoes");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "ShoesSizes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "ShoesSizes");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Shoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
