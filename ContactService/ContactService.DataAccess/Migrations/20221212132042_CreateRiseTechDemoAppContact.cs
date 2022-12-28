using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateRiseTechDemoAppContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(20)", nullable: false),
                    surname = table.Column<string>(type: "varchar(20)", nullable: false),
                    firm = table.Column<string>(type: "varchar(50)", nullable: false),
                    createdate = table.Column<DateTime>(name: "create_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contact_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    personid = table.Column<Guid>(name: "person_id", type: "uuid", nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    location = table.Column<string>(type: "varchar(20)", nullable: false),
                    info = table.Column<string>(type: "varchar(1000)", nullable: false),
                    createdate = table.Column<DateTime>(name: "create_date", type: "timestamp with time zone", nullable: false),
                    Updatedate = table.Column<DateTime>(name: "Update_date", type: "timestamp with time zone", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_info_people_person_id",
                        column: x => x.personid,
                        principalTable: "people",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contact_info_person_id",
                table: "contact_info",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact_info");

            migrationBuilder.DropTable(
                name: "people");
        }
    }
}
