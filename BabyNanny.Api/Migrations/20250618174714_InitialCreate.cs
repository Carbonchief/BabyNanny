using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabyNanny.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Child",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Child", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stopped = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FeedingType = table.Column<int>(type: "int", nullable: true),
                    AmountML = table.Column<int>(type: "int", nullable: true),
                    BottleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MealDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaperType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyAction_Child_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Child",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyAction_ChildId",
                table: "BabyAction",
                column: "ChildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyAction");

            migrationBuilder.DropTable(
                name: "Child");
        }
    }
}
