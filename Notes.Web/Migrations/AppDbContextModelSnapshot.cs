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

                    b.HasKey("Id");

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

                    b.ToTable("LabelHistory");
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

                    b.HasKey("Id");

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

                    b.ToTable("NoteHistory");
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

            modelBuilder.Entity("Notes.Core.Entities.LabelHistory", b =>
                {
                    b.HasOne("Notes.Core.Entities.Label", "Label")
                        .WithMany("Histories")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Label");
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

            modelBuilder.Entity("Notes.Core.Entities.Label", b =>
                {
                    b.Navigation("Histories");

                    b.Navigation("NoteLabels");
                });

            modelBuilder.Entity("Notes.Core.Entities.Note", b =>
                {
                    b.Navigation("Histories");

                    b.Navigation("NoteLabels");
                });
#pragma warning restore 612, 618
        }
    }
}
