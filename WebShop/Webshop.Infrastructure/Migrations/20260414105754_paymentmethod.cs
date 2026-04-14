using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class paymentmethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BetalningsMetod",
                table: "Ordrar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetalningsMetod",
                table: "Ordrar");
        }
    }
}
