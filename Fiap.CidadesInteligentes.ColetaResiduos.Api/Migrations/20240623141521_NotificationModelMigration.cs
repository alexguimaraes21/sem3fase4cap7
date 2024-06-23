using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTableNotification : Migration
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
                name: "NET_NOTIFICATIONS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOTIFICATION_TYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MESSAGE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    VALID_UNTIL = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "NUMBER(1,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_NOTIFICATIONS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_NOTIFICATIONS");

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
