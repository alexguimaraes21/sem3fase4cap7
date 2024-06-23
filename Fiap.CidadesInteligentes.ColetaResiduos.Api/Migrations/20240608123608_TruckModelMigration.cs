using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class TruckModelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NET_TRUCKS",
                columns: table => new
                {
                    TRUCK_ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LICENSE_PLATE = table.Column<string>(type: "CHAR(7)", nullable: false),
                    CAPACITY = table.Column<decimal>(type: "FLOAT(38)", nullable: false),
                    AVAILABLE = table.Column<bool>(type: "NUMBER(1,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_TRUCKS", x => x.TRUCK_ID);
                });

            // Adding Trucks
            var sqlInsTruck1 = "INSERT INTO NET_TRUCKS (LICENSE_PLATE, CAPACITY, AVAILABLE) VALUES" +
                "('ADG8737', 15000.0, 1)";
            migrationBuilder.Sql(sqlInsTruck1);
            
            var sqlInsTruck2 = "INSERT INTO NET_TRUCKS (LICENSE_PLATE, CAPACITY, AVAILABLE) VALUES" +
                "('FTB7157', 15000.0, 1)";
            migrationBuilder.Sql(sqlInsTruck2);

            var sqlInsTruck3 = "INSERT INTO NET_TRUCKS (LICENSE_PLATE, CAPACITY, AVAILABLE) VALUES" +
                "('GTJ5375', 23000.0, 1)";
            migrationBuilder.Sql(sqlInsTruck3);

            var sqlInsTruck4 = "INSERT INTO NET_TRUCKS (LICENSE_PLATE, CAPACITY, AVAILABLE) VALUES" +
                "('ENB8L24', 25000.0, 1)";
            migrationBuilder.Sql(sqlInsTruck4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_TRUCKS");
        }
    }
}
