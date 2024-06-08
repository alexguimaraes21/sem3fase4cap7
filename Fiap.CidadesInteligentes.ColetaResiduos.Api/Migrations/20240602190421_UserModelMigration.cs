using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserModelMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NET_USERS",
                columns: table => new
                {
                    USER_ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMAIL = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    PASSWORD = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "DATE", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "NUMBER(1,0)", nullable: false),
                    ROLE = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_USERS", x => x.USER_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NET_USERS_EMAIL",
                table: "NET_USERS",
                column: "EMAIL",
                unique: true);

            // Adding Users to database
            // Admin
            var sqlInsAdmin = "INSERT INTO NET_USERS (EMAIL, PASSWORD, CREATED_AT, IS_ACTIVE, ROLE) VALUES" +
                "('admin@email.com.br','pass123',SYSDATE,1,'Admin')";
            migrationBuilder.Sql(sqlInsAdmin);

            // Manager
            var sqlInsManager = "INSERT INTO NET_USERS (EMAIL, PASSWORD, CREATED_AT, IS_ACTIVE, ROLE) VALUES" +
                "('manager@email.com.br','pass123',SYSDATE,1,'Manager')";
            migrationBuilder.Sql(sqlInsManager);

            // User
            var sqlInsUser = "INSERT INTO NET_USERS (EMAIL, PASSWORD, CREATED_AT, IS_ACTIVE, ROLE) VALUES" +
                "('user@email.com.br','pass123',SYSDATE,1,'User')";
            migrationBuilder.Sql(sqlInsUser);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_USERS");
        }
    }
}
