using Microsoft.EntityFrameworkCore.Migrations;

namespace BatmansInventory.API.Migrations
{
    public partial class PopulateItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Items (PartName, PartNumber, OrderLeadTime, QuantityOnHand, SafetyStock, Created, CreatedBy) " +
                                            "VALUES ('Batarang', 'BTG-001', 3, 10, 4, '11-23-2019', 'Lucius Fox')");
            migrationBuilder.Sql("INSERT INTO Items (PartName, PartNumber, OrderLeadTime, QuantityOnHand, SafetyStock, Created, CreatedBy) " +
                                            "VALUES ('Bat-Bolas', 'BBO-102', 5, 7, 2, '11-23-2019', 'Lucius Fox')");
            migrationBuilder.Sql("INSERT INTO Items (PartName, PartNumber, OrderLeadTime, QuantityOnHand, SafetyStock, Created, CreatedBy) " +
                                            "VALUES ('Flash-Bang Grenades', 'FBG-081', 6, 12, 5, '11-22-2019', 'Alfred Pennyworth')");
            migrationBuilder.Sql("INSERT INTO Items (PartName, PartNumber, OrderLeadTime, QuantityOnHand, SafetyStock, Created, CreatedBy) " +
                                            "VALUES ('Grapple Gun', 'GGU-307', 10, 1, 2, '11-22-2019', 'Alfred Pennyworth')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
