using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CitiesMap.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogisticCenter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Road",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distance = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    FromId = table.Column<int>(nullable: true),
                    ToId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Road", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Road_City_FromId",
                        column: x => x.FromId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Road_City_ToId",
                        column: x => x.ToId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "City",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Road_FromId",
                table: "Road",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Road_ToId",
                table: "Road",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogisticCenter");

            migrationBuilder.DropTable(
                name: "Road");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
