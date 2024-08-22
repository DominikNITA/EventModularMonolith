﻿// <auto-generated />
using System;
using EventModularMonolith.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventModularMonolith.Modules.Users.Infrastructure.Database.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20240822161139_FixModeratorsTable")]
    partial class FixModeratorsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EventModularMonolith.Modules.Users.Domain.Organizers.Organizer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_organizer");

                    b.ToTable("organizer", "users");
                });

            modelBuilder.Entity("EventModularMonolith.Modules.Users.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", "users");
                });

            modelBuilder.Entity("EventModularMonolith.Shared.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_inbox_messages");

                    b.ToTable("inbox_messages", "users");
                });

            modelBuilder.Entity("EventModularMonolith.Shared.Infrastructure.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("InboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("inbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("InboxMessageId", "Name")
                        .HasName("pk_inbox_message_consumers");

                    b.ToTable("inbox_message_consumers", "users");
                });

            modelBuilder.Entity("EventModularMonolith.Shared.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", "users");
                });

            modelBuilder.Entity("EventModularMonolith.Shared.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("OutboxMessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("outbox_message_id");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("OutboxMessageId", "Name")
                        .HasName("pk_outbox_message_consumers");

                    b.ToTable("outbox_message_consumers", "users");
                });

            modelBuilder.Entity("EventModularMonolith.Modules.Users.Domain.Organizers.Organizer", b =>
                {
                    b.OwnsMany("EventModularMonolith.Modules.Users.Domain.Organizers.Moderator", "Moderators", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("user_id");

                            b1.Property<Guid>("OrganizerId")
                                .HasColumnType("uuid")
                                .HasColumnName("organizer_id");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("boolean")
                                .HasColumnName("is_active");

                            b1.HasKey("UserId", "OrganizerId")
                                .HasName("pk_moderators");

                            b1.HasIndex("OrganizerId")
                                .HasDatabaseName("ix_moderators_organizer_id");

                            b1.ToTable("moderators", "users");

                            b1.WithOwner()
                                .HasForeignKey("OrganizerId")
                                .HasConstraintName("fk_moderators_organizer_organizer_id");

                            b1.OwnsOne("EventModularMonolith.Modules.Users.Domain.Organizers.ModeratorRole", "Role", b2 =>
                                {
                                    b2.Property<Guid>("ModeratorUserId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("user_id");

                                    b2.Property<Guid>("ModeratorOrganizerId")
                                        .HasColumnType("uuid")
                                        .HasColumnName("organizer_id");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("text")
                                        .HasColumnName("Role");

                                    b2.HasKey("ModeratorUserId", "ModeratorOrganizerId");

                                    b2.ToTable("moderators", "users");

                                    b2.WithOwner()
                                        .HasForeignKey("ModeratorUserId", "ModeratorOrganizerId")
                                        .HasConstraintName("fk_moderators_moderators_user_id_organizer_id");
                                });

                            b1.Navigation("Role")
                                .IsRequired();
                        });

                    b.Navigation("Moderators");
                });
#pragma warning restore 612, 618
        }
    }
}
