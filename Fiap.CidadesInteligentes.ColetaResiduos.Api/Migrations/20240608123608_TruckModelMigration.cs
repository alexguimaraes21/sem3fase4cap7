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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_TRUCKS");
        }
    }
}
