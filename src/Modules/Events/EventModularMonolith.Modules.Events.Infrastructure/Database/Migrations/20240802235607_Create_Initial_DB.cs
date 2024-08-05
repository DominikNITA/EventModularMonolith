﻿//<auto-generated/>
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventModularMonolith.Modules.Events.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Create_Initial_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "speakers",
                schema: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_speakers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_speaker",
                schema: "events",
                columns: table => new
                {
                    events_id = table.Column<Guid>(type: "uuid", nullable: false),
                    speakers_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_speaker", x => new { x.events_id, x.speakers_id });
                    table.ForeignKey(
                        name: "fk_event_speaker_events_events_id",
                        column: x => x.events_id,
                        principalSchema: "events",
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_speaker_speakers_speakers_id",
                        column: x => x.speakers_id,
                        principalSchema: "events",
                        principalTable: "speakers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
               name: "links",
               schema: "events",
               columns: table => new
               {
                  id = table.Column<Guid>(type: "uuid", nullable: false),
                  url = table.Column<string>(type: "text", nullable: false),
                  speaker_id = table.Column<Guid>(type: "uuid", nullable: true)
               },
               constraints: table =>
               {
                  table.PrimaryKey("pk_links", x => x.id);
                  table.ForeignKey(
                     name: "fk_links_speakers_speaker_id",
                     column: x => x.speaker_id,
                     principalSchema: "events",
                     principalTable: "speakers",
                     principalColumn: "id");
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_speaker",
                schema: "events");

            migrationBuilder.DropTable(
                name: "links",
                schema: "events");

            migrationBuilder.DropTable(
                name: "speakers",
                schema: "events");

            migrationBuilder.AddForeignKey(
                name: "fk_ticket_types_events_event_id",
                schema: "events",
                table: "ticket_types",
                column: "event_id",
                principalSchema: "events",
                principalTable: "events",
                principalColumn: "venue_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
