﻿//<auto-generated/>
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventModularMonolith.Modules.Events.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Organizer_Venue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "organizer_id",
                schema: "events",
                table: "venues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "organizer_id",
                schema: "events",
                table: "venues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
