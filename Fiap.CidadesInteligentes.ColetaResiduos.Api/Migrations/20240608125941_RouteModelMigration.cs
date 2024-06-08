using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class RouteModelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CAPACITY",
                table: "NET_TRUCKS",
                type: "FLOAT(38)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT");

            migrationBuilder.AlterColumn<decimal>(
                name: "CAPACITY",
                table: "NET_CONTAINERS",
                type: "FLOAT(38)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT");

            migrationBuilder.CreateTable(
                name: "NET_ROUTES",
                columns: table => new
                {
                    ROUTE_ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    START_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    END_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    TruckId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_ROUTES", x => x.ROUTE_ID);
                    table.ForeignKey(
                        name: "FK_NET_ROUTES_NET_TRUCKS_TruckId",
                        column: x => x.TruckId,
                        principalTable: "NET_TRUCKS",
                        principalColumn: "TRUCK_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NET_ROUTES_TruckId",
                table: "NET_ROUTES",
                column: "TruckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_ROUTES");

            migrationBuilder.AlterColumn<decimal>(
                name: "CAPACITY",
                table: "NET_TRUCKS",
                type: "FLOAT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT(38)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CAPACITY",
                table: "NET_CONTAINERS",
                type: "FLOAT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT(38)");
        }
    }
}
