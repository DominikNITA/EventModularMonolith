﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventModularMonolith.Modules.Users.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Organizers_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_moderators_organizer_organizer_id",
                schema: "users",
                table: "moderators");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organizer",
                schema: "users",
                table: "organizer");

            migrationBuilder.RenameTable(
                name: "organizer",
                schema: "users",
                newName: "organizers",
                newSchema: "users");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organizers",
                schema: "users",
                table: "organizers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_moderators_organizers_organizer_id",
                schema: "users",
                table: "moderators",
                column: "organizer_id",
                principalSchema: "users",
                principalTable: "organizers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_moderators_organizers_organizer_id",
                schema: "users",
                table: "moderators");

            migrationBuilder.DropPrimaryKey(
                name: "pk_organizers",
                schema: "users",
                table: "organizers");

            migrationBuilder.RenameTable(
                name: "organizers",
                schema: "users",
                newName: "organizer",
                newSchema: "users");

            migrationBuilder.AddPrimaryKey(
                name: "pk_organizer",
                schema: "users",
                table: "organizer",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_moderators_organizer_organizer_id",
                schema: "users",
                table: "moderators",
                column: "organizer_id",
                principalSchema: "users",
                principalTable: "organizer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
