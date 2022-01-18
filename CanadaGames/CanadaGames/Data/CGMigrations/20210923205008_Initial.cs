using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CanadaGames.Data.CGMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CG");

            migrationBuilder.CreateTable(
                name: "Coaches",
                schema: "CG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contingents",
                schema: "CG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contingents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "CG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sports",
                schema: "CG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 3, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sports", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                schema: "CG",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    AthleteCode = table.Column<string>(maxLength: 7, nullable: true),
                    Hometown = table.Column<string>(maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    YearsInSport = table.Column<int>(nullable: false),
                    Affiliation = table.Column<string>(maxLength: 255, nullable: false),
                    Goals = table.Column<string>(nullable: false),
                    Position = table.Column<string>(maxLength: 50, nullable: true),
                    RoleModel = table.Column<string>(maxLength: 100, nullable: true),
                    MedalInfo = table.Column<string>(maxLength: 2000, nullable: true),
                    ContingentID = table.Column<int>(nullable: false),
                    SportID = table.Column<int>(nullable: false),
                    GenderID = table.Column<int>(nullable: false),
                    CoachID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Athletes_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalSchema: "CG",
                        principalTable: "Coaches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Contingents_ContingentID",
                        column: x => x.ContingentID,
                        principalSchema: "CG",
                        principalTable: "Contingents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Genders_GenderID",
                        column: x => x.GenderID,
                        principalSchema: "CG",
                        principalTable: "Genders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Sports_SportID",
                        column: x => x.SportID,
                        principalSchema: "CG",
                        principalTable: "Sports",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_AthleteCode",
                schema: "CG",
                table: "Athletes",
                column: "AthleteCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_CoachID",
                schema: "CG",
                table: "Athletes",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_ContingentID",
                schema: "CG",
                table: "Athletes",
                column: "ContingentID");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_GenderID",
                schema: "CG",
                table: "Athletes",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_SportID",
                schema: "CG",
                table: "Athletes",
                column: "SportID");

            migrationBuilder.CreateIndex(
                name: "IX_Contingents_Code",
                schema: "CG",
                table: "Contingents",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Code",
                schema: "CG",
                table: "Genders",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_Code",
                schema: "CG",
                table: "Sports",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Athletes",
                schema: "CG");

            migrationBuilder.DropTable(
                name: "Coaches",
                schema: "CG");

            migrationBuilder.DropTable(
                name: "Contingents",
                schema: "CG");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "CG");

            migrationBuilder.DropTable(
                name: "Sports",
                schema: "CG");
        }
    }
}
