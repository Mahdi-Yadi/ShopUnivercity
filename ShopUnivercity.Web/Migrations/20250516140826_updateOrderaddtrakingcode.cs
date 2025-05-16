using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopUnivercity.Web.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderaddtrakingcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrakingCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrakingCode",
                table: "Orders");
        }
    }
}
