using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class bc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CombatCharacters",
                table: "CombatCharacters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CombatCharacters",
                table: "CombatCharacters",
                columns: new[] { "CombatID", "CharacterID", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CombatCharacters",
                table: "CombatCharacters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CombatCharacters",
                table: "CombatCharacters",
                columns: new[] { "CombatID", "CharacterID" });
        }
    }
}
