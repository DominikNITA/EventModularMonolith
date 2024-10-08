﻿//<auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventModularMonolith.Modules.Events.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Links : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_links_speakers_speaker_id",
                schema: "events",
                table: "links");

            migrationBuilder.DropPrimaryKey(
                name: "pk_links",
                schema: "events",
                table: "links");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "events",
                table: "links");

            migrationBuilder.AlterColumn<Guid>(
                name: "speaker_id",
                schema: "events",
                table: "links",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_links",
                schema: "events",
                table: "links",
                columns: new[] { "url", "speaker_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_links_speakers_speaker_id",
                schema: "events",
                table: "links",
                column: "speaker_id",
                principalSchema: "events",
                principalTable: "speakers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_links_speakers_speaker_id",
                schema: "events",
                table: "links");

            migrationBuilder.DropPrimaryKey(
                name: "pk_links",
                schema: "events",
                table: "links");

            migrationBuilder.AlterColumn<Guid>(
                name: "speaker_id",
                schema: "events",
                table: "links",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "events",
                table: "links",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_links",
                schema: "events",
                table: "links",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_links_speakers_speaker_id",
                schema: "events",
                table: "links",
                column: "speaker_id",
                principalSchema: "events",
                principalTable: "speakers",
                principalColumn: "id");
        }
    }
}
