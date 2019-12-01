using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BatmansInventory.API.Migrations
{
    public partial class ChangeItemToInventoryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalItems_InventoryItems_ItemId",
                table: "PhysicalItems");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalItems_ItemId",
                table: "PhysicalItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PhysicalItems");

            migrationBuilder.AddColumn<int>(
                name: "InventoryItemId",
                table: "PhysicalItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalItems_InventoryItemId",
                table: "PhysicalItems",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalItems_InventoryItems_InventoryItemId",
                table: "PhysicalItems",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalItems_InventoryItems_InventoryItemId",
                table: "PhysicalItems");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalItems_InventoryItemId",
                table: "PhysicalItems");

            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                table: "PhysicalItems");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PhysicalItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalItems_ItemId",
                table: "PhysicalItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalItems_InventoryItems_ItemId",
                table: "PhysicalItems",
                column: "ItemId",
                principalTable: "InventoryItems",
                principalColumn: "InventoryItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
