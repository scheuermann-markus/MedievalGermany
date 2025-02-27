using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedievalGermany.Migrations
{
    /// <inheritdoc />
    public partial class INITIAL_CREATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Castles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Eroeffnet = table.Column<int>(type: "integer", nullable: true),
                    WikipediaUrl = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Geolocation_Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Geolocation_Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Castles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Castles");
        }
    }
}
