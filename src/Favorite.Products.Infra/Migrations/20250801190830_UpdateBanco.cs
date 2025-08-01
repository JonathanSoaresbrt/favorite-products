using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Favorite.Products.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "product_id",
                table: "favorite_product",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_id",
                table: "favorite_product");
        }
    }
}
