using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LPSManagement.Server.Migrations
{
    public partial class removeLPSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LPSheets");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Employes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Employes");

            migrationBuilder.CreateTable(
                name: "LPSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LPSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LPSheets_Employes_EmployeId",
                        column: x => x.EmployeId,
                        principalTable: "Employes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LPSheets_EmployeId",
                table: "LPSheets",
                column: "EmployeId",
                unique: true);
        }
    }
}
