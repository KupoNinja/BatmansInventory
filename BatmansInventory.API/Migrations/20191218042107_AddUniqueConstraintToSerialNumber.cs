using Microsoft.EntityFrameworkCore.Migrations;

namespace BatmansInventory.API.Migrations
{
    public partial class AddUniqueConstraintToSerialNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "PhysicalItems",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalItems_SerialNumber",
                table: "PhysicalItems",
                column: "SerialNumber",
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhysicalItems_SerialNumber",
                table: "PhysicalItems");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "PhysicalItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
