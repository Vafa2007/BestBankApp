using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestBankApp.Migrations
{
    public partial class BestBankAppV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BIRTHDAY = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SALARY = table.Column<float>(type: "real", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CREDIT_APPLY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLIENT_ID = table.Column<int>(type: "int", nullable: false),
                    AMOUNT_OF_CREDIT = table.Column<float>(type: "real", nullable: false),
                    TERMS_IN_MONTHS = table.Column<int>(type: "int", nullable: false),
                    RESULT = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CREDIT_APPLY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CREDIT_APPLY_CLIENTS_CLIENT_ID",
                        column: x => x.CLIENT_ID,
                        principalTable: "CLIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CREDIT_APPLY_CLIENT_ID",
                table: "CREDIT_APPLY",
                column: "CLIENT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CREDIT_APPLY");

            migrationBuilder.DropTable(
                name: "CLIENTS");
        }
    }
}
