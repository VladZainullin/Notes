﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notes.Data.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Notes.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Notes.Core.Entities.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("Notes.Core.Entities.LabelHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfModification")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("LabelId")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LabelId");

                    b.ToTable("LabelHistories");
                });

            modelBuilder.Entity("Notes.Core.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<string>("Header")
                        .HasColumnType("text");

                    b.Property<bool>("IsPinned")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Notes.Core.Entities.NoteHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfModification")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Header")
                        .HasColumnType("text");

                    b.Property<int>("NoteId")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("NoteHistories");
                });

            modelBuilder.Entity("Notes.Core.Entities.NoteLabel", b =>
                {
                    b.Property<int>("LabelId")
                        .HasColumnType("integer");

                    b.Property<int>("NoteId")
                        .HasColumnType("integer");

                    b.HasKey("LabelId", "NoteId");

                    b.HasIndex("NoteId");

                    b.ToTable("NoteLabels");
                });

            modelBuilder.Entity("Notes.Core.Entities.Reminder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("NoteId")
                        .HasColumnType("integer");

                    b.Property<TimeOnly>("TimeOfInclusion")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Reminder");
                });

            modelBuilder.Entity("Notes.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Notes.Core.Entities.Label", b =>
                {
                    b.HasOne("Notes.Core.Entities.User", "User")
                        .WithMany("Labels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Notes.Core.Entities.LabelHistory", b =>
                {
                    b.HasOne("Notes.Core.Entities.Label", "Label")
                        .WithMany("Histories")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Label");
                });

            modelBuilder.Entity("Notes.Core.Entities.Note", b =>
                {
                    b.HasOne("Notes.Core.Entities.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Notes.Core.Entities.NoteHistory", b =>
                {
                    b.HasOne("Notes.Core.Entities.Note", "Note")
                        .WithMany("Histories")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Notes.Core.Entities.NoteLabel", b =>
                {
                    b.HasOne("Notes.Core.Entities.Label", "Label")
                        .WithMany("NoteLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notes.Core.Entities.Note", "Note")
                        .WithMany("NoteLabels")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Label");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Notes.Core.Entities.Reminder", b =>
                {
                    b.HasOne("Notes.Core.Entities.Note", "Note")
                        .WithMany("Reminders")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("Notes.Core.Entities.Label", b =>
                {
                    b.Navigation("Histories");

                    b.Navigation("NoteLabels");
                });

            modelBuilder.Entity("Notes.Core.Entities.Note", b =>
                {
                    b.Navigation("Histories");

                    b.Navigation("NoteLabels");

                    b.Navigation("Reminders");
                });

            modelBuilder.Entity("Notes.Core.Entities.User", b =>
                {
                    b.Navigation("Labels");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
