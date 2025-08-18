using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourEcommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SportImage",
                table: "Sports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenderImage",
                table: "Genders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandImage",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportImage",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "GenderImage",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "BrandImage",
                table: "Brands");
        }
    }
}
