using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateRiseTechDemoAppReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: true),
                    filepath = table.Column<string>(name: "file_path", type: "varchar(20)", nullable: true),
                    createdate = table.Column<DateTime>(name: "create_date", type: "timestamp without time zone", nullable: false),
                    completedate = table.Column<DateTime>(name: "complete_date", type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reports");
        }
    }
}
