using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreDbContext.Migrations
{
    public partial class fixtablenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SamuraiBattle_Samurais_SamuraiId",
                table: "SamuraiBattle");

            migrationBuilder.DropForeignKey(
                name: "FK_Sword_Makers_MakerId",
                table: "Sword");

            migrationBuilder.DropForeignKey(
                name: "FK_Sword_Samurais_SamuraiId",
                table: "Sword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sword",
                table: "Sword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SamuraiBattle",
                table: "SamuraiBattle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Swords",
                table: "Sword",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SamuraiBattles",
                table: "SamuraiBattle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SamuraiBattles_Samurais_SamuraiId",
                table: "SamuraiBattle",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Swords_Makers_MakerId",
                table: "Sword",
                column: "MakerId",
                principalTable: "Makers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Swords_Samurais_SamuraiId",
                table: "Sword",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Sword_SamuraiId",
                table: "Sword",
                newName: "IX_Swords_SamuraiId");

            migrationBuilder.RenameIndex(
                name: "IX_Sword_MakerId",
                table: "Sword",
                newName: "IX_Swords_MakerId");

            migrationBuilder.RenameIndex(
                name: "IX_SamuraiBattle_SamuraiId",
                table: "SamuraiBattle",
                newName: "IX_SamuraiBattles_SamuraiId");

            migrationBuilder.RenameTable(
                name: "Sword",
                newName: "Swords");

            migrationBuilder.RenameTable(
                name: "SamuraiBattle",
                newName: "SamuraiBattles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SamuraiBattles_Samurais_SamuraiId",
                table: "SamuraiBattles");

            migrationBuilder.DropForeignKey(
                name: "FK_Swords_Makers_MakerId",
                table: "Swords");

            migrationBuilder.DropForeignKey(
                name: "FK_Swords_Samurais_SamuraiId",
                table: "Swords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Swords",
                table: "Swords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SamuraiBattles",
                table: "SamuraiBattles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sword",
                table: "Swords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SamuraiBattle",
                table: "SamuraiBattles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SamuraiBattle_Samurais_SamuraiId",
                table: "SamuraiBattles",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sword_Makers_MakerId",
                table: "Swords",
                column: "MakerId",
                principalTable: "Makers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sword_Samurais_SamuraiId",
                table: "Swords",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Swords_SamuraiId",
                table: "Swords",
                newName: "IX_Sword_SamuraiId");

            migrationBuilder.RenameIndex(
                name: "IX_Swords_MakerId",
                table: "Swords",
                newName: "IX_Sword_MakerId");

            migrationBuilder.RenameIndex(
                name: "IX_SamuraiBattles_SamuraiId",
                table: "SamuraiBattles",
                newName: "IX_SamuraiBattle_SamuraiId");

            migrationBuilder.RenameTable(
                name: "Swords",
                newName: "Sword");

            migrationBuilder.RenameTable(
                name: "SamuraiBattles",
                newName: "SamuraiBattle");
        }
    }
}
