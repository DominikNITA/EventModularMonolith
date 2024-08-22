﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventModularMonolith.Modules.Users.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "organizer",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Moderators",
                schema: "users",
                columns: table => new
                {
                    organizer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_moderators", x => new { x.user_id, x.organizer_id });
                    table.ForeignKey(
                        name: "fk_moderators_organizer_organizer_id",
                        column: x => x.organizer_id,
                        principalSchema: "users",
                        principalTable: "organizer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_moderators_organizer_id",
                schema: "users",
                table: "Moderators",
                column: "organizer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moderators",
                schema: "users");

            migrationBuilder.DropTable(
                name: "organizer",
                schema: "users");
        }
    }
}
