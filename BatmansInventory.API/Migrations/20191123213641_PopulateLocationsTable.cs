using Microsoft.EntityFrameworkCore.Migrations;

namespace BatmansInventory.API.Migrations
{
    public partial class PopulateLocationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Locations (City, State) VALUES ('Boise', 'Idaho')");
            migrationBuilder.Sql("INSERT INTO Locations (City, State) VALUES ('Lansing', 'Michigan')");
            migrationBuilder.Sql("INSERT INTO Locations (City, State) VALUES ('Chicago', 'Illinois')");
            migrationBuilder.Sql("INSERT INTO Locations (City, State) VALUES ('Bonita Springs', 'Florida')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
