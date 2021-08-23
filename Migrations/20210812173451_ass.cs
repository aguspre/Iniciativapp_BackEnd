using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class ass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterDB",
                columns: table => new
                {
                    CharacterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false),
                    UrlAvatarImg = table.Column<string>(type: "varchar(100)", nullable: true),
                    HitPoints = table.Column<int>(type: "int", nullable: true),
                    ArmorClass = table.Column<int>(type: "int", nullable: true),
                    IsPlayable = table.Column<bool>(type: "bit", nullable: true),
                    IsDeletable = table.Column<bool>(type: "bit", nullable: false),
                    Creator = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterDB", x => x.CharacterID);
                });

            migrationBuilder.CreateTable(
                name: "CombatDB",
                columns: table => new
                {
                    CombatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DofCombat = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatDB", x => x.CombatID);
                });

            migrationBuilder.CreateTable(
                name: "CombatCharacters",
                columns: table => new
                {
                    CombatID = table.Column<int>(type: "int", nullable: false),
                    CharacterID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatCharacters", x => new { x.CombatID, x.CharacterID });
                    table.ForeignKey(
                        name: "FK_CombatCharacters_CharacterDB_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "CharacterDB",
                        principalColumn: "CharacterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatCharacters_CombatDB_CombatID",
                        column: x => x.CombatID,
                        principalTable: "CombatDB",
                        principalColumn: "CombatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombatCharacters_CharacterID",
                table: "CombatCharacters",
                column: "CharacterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombatCharacters");

            migrationBuilder.DropTable(
                name: "CharacterDB");

            migrationBuilder.DropTable(
                name: "CombatDB");
        }
    }
}
