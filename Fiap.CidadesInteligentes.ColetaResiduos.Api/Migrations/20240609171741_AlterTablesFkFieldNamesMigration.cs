using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablesFkFieldNamesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NET_COLLECTIONS_NET_CONTAINERS_ContainerId",
                table: "NET_COLLECTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_NET_COLLECTIONS_NET_ROUTES_RouteId",
                table: "NET_COLLECTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_NET_ROUTES_NET_TRUCKS_TruckId",
                table: "NET_ROUTES");

            migrationBuilder.RenameColumn(
                name: "TruckId",
                table: "NET_ROUTES",
                newName: "TRUCK_ID");

            migrationBuilder.RenameIndex(
                name: "IX_NET_ROUTES_TruckId",
                table: "NET_ROUTES",
                newName: "IX_NET_ROUTES_TRUCK_ID");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "NET_COLLECTIONS",
                newName: "ROUTE_ID");

            migrationBuilder.RenameColumn(
                name: "ContainerId",
                table: "NET_COLLECTIONS",
                newName: "CONTAINER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_NET_COLLECTIONS_RouteId",
                table: "NET_COLLECTIONS",
                newName: "IX_NET_COLLECTIONS_ROUTE_ID");

            migrationBuilder.RenameIndex(
                name: "IX_NET_COLLECTIONS_ContainerId",
                table: "NET_COLLECTIONS",
                newName: "IX_NET_COLLECTIONS_CONTAINER_ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NET_COLLECTIONS_NET_CONTAINERS_CONTAINER_ID",
                table: "NET_COLLECTIONS",
                column: "CONTAINER_ID",
                principalTable: "NET_CONTAINERS",
                principalColumn: "CONTAINER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NET_COLLECTIONS_NET_ROUTES_ROUTE_ID",
                table: "NET_COLLECTIONS",
                column: "ROUTE_ID",
                principalTable: "NET_ROUTES",
                principalColumn: "ROUTE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NET_ROUTES_NET_TRUCKS_TRUCK_ID",
                table: "NET_ROUTES",
                column: "TRUCK_ID",
                principalTable: "NET_TRUCKS",
                principalColumn: "TRUCK_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NET_COLLECTIONS_NET_CONTAINERS_CONTAINER_ID",
                table: "NET_COLLECTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_NET_COLLECTIONS_NET_ROUTES_ROUTE_ID",
                table: "NET_COLLECTIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_NET_ROUTES_NET_TRUCKS_TRUCK_ID",
                table: "NET_ROUTES");

            migrationBuilder.RenameColumn(
                name: "TRUCK_ID",
                table: "NET_ROUTES",
                newName: "TruckId");

            migrationBuilder.RenameIndex(
                name: "IX_NET_ROUTES_TRUCK_ID",
                table: "NET_ROUTES",
                newName: "IX_NET_ROUTES_TruckId");

            migrationBuilder.RenameColumn(
                name: "ROUTE_ID",
                table: "NET_COLLECTIONS",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "CONTAINER_ID",
                table: "NET_COLLECTIONS",
                newName: "ContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_NET_COLLECTIONS_ROUTE_ID",
                table: "NET_COLLECTIONS",
                newName: "IX_NET_COLLECTIONS_RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_NET_COLLECTIONS_CONTAINER_ID",
                table: "NET_COLLECTIONS",
                newName: "IX_NET_COLLECTIONS_ContainerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_NET_COLLECTIONS_NET_CONTAINERS_ContainerId",
                table: "NET_COLLECTIONS",
                column: "ContainerId",
                principalTable: "NET_CONTAINERS",
                principalColumn: "CONTAINER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NET_COLLECTIONS_NET_ROUTES_RouteId",
                table: "NET_COLLECTIONS",
                column: "RouteId",
                principalTable: "NET_ROUTES",
                principalColumn: "ROUTE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NET_ROUTES_NET_TRUCKS_TruckId",
                table: "NET_ROUTES",
                column: "TruckId",
                principalTable: "NET_TRUCKS",
                principalColumn: "TRUCK_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
