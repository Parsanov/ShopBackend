using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExternalOfferId",
                table: "ShoesSizes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Shoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ExternalGroupId",
                table: "Shoes",
                column: "ExternalGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_Price",
                table: "Shoes",
                column: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shoes_ExternalGroupId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_Price",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Shoes");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalOfferId",
                table: "ShoesSizes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
