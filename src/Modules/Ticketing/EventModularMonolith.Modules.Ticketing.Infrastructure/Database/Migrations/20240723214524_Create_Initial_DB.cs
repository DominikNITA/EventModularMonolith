﻿//<autogenerated/>
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventModularMonolith.Modules.Ticketing.Infrastructure.Database.Migrations
{
   /// <inheritdoc />
   public partial class Create_Initial_DB : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.EnsureSchema(
             name: "ticketing");

         migrationBuilder.CreateTable(
             name: "customers",
             schema: "ticketing",
             columns: table => new
             {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                first_name = table.Column<string>(type: "text", nullable: false),
                last_name = table.Column<string>(type: "text", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("pk_customers", x => x.id);
             });

         migrationBuilder.CreateTable(
             name: "events",
             schema: "ticketing",
             columns: table => new
             {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "text", nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                location = table.Column<string>(type: "text", nullable: false),
                starts_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ends_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                canceled = table.Column<bool>(type: "boolean", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("pk_events", x => x.id);
             });

         migrationBuilder.CreateTable(
             name: "ticket_types",
             schema: "ticketing",
             columns: table => new
             {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                event_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                price = table.Column<decimal>(type: "numeric", nullable: false),
                currency = table.Column<string>(type: "text", nullable: false),
                quantity = table.Column<decimal>(type: "numeric", nullable: false),
                available_quantity = table.Column<decimal>(type: "numeric", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("pk_ticket_types", x => x.id);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "customers",
             schema: "ticketing");

         migrationBuilder.DropTable(
             name: "events",
             schema: "ticketing");

         migrationBuilder.DropTable(
             name: "ticket_types",
             schema: "ticketing");
      }
   }
}
