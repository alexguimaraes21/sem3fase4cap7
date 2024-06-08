using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class ContainerModelMigration : Migration
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

            migrationBuilder.CreateTable(
                name: "NET_CONTAINERS",
                columns: table => new
                {
                    CONTAINER_ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LOCATION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CAPACITY = table.Column<decimal>(type: "FLOAT(38)", nullable: false),
                    CURRENT_LEVEL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_CONTAINERS", x => x.CONTAINER_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_CONTAINERS");

            migrationBuilder.AlterColumn<decimal>(
                name: "CAPACITY",
                table: "NET_TRUCKS",
                type: "FLOAT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "FLOAT(38)");
        }
    }
}
