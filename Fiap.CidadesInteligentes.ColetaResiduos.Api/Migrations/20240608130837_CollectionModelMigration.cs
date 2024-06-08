using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class CollectionModelMigration : Migration
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
                name: "NET_COLLECTIONS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATE_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ContainerId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    RouteId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_COLLECTIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NET_COLLECTIONS_NET_CONTAINERS_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "NET_CONTAINERS",
                        principalColumn: "CONTAINER_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NET_COLLECTIONS_NET_ROUTES_RouteId",
                        column: x => x.RouteId,
                        principalTable: "NET_ROUTES",
                        principalColumn: "ROUTE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NET_COLLECTIONS_ContainerId",
                table: "NET_COLLECTIONS",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_NET_COLLECTIONS_RouteId",
                table: "NET_COLLECTIONS",
                column: "RouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_COLLECTIONS");

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
