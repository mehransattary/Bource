using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class addModels_Nomad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityTracker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationState = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTracker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nomads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NomadDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomadId = table.Column<int>(type: "int", nullable: false),
                    NomadId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomadDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NomadDates_Nomads_NomadId1",
                        column: x => x.NomadId1,
                        principalTable: "Nomads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomadActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomadDateId = table.Column<int>(type: "int", nullable: false),
                    NomadDateId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomadActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NomadActions_NomadDates_NomadDateId1",
                        column: x => x.NomadDateId1,
                        principalTable: "NomadDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NomadActions_NomadDateId1",
                table: "NomadActions",
                column: "NomadDateId1");

            migrationBuilder.CreateIndex(
                name: "IX_NomadDates_NomadId1",
                table: "NomadDates",
                column: "NomadId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTracker");

            migrationBuilder.DropTable(
                name: "NomadActions");

            migrationBuilder.DropTable(
                name: "NomadDates");

            migrationBuilder.DropTable(
                name: "Nomads");
        }
    }
}
