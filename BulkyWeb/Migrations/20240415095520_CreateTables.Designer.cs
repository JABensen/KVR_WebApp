﻿// <auto-generated />
using System;
using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BulkyWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240415095520_CreateTables")]
    partial class CreateTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BulkyWeb.Models.AssociatedTaskIndex", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<Guid>("MetricId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MetricId");

                    b.ToTable("AssociatedTasks");
                });

            modelBuilder.Entity("BulkyWeb.Models.EventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("SecondsIntoTest")
                        .HasColumnType("float");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("EventLogs");
                });

            modelBuilder.Entity("BulkyWeb.Models.ObjectMetric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActiveTask")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActiveTaskDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("ObjectMetrics");
                });

            modelBuilder.Entity("BulkyWeb.Models.Professor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClassKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("BulkyWeb.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClassKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("BulkyWeb.Models.AssociatedTaskIndex", b =>
                {
                    b.HasOne("BulkyWeb.Models.ObjectMetric", "ObjectMetric")
                        .WithMany("associatedTaskIndexes")
                        .HasForeignKey("MetricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObjectMetric");
                });

            modelBuilder.Entity("BulkyWeb.Models.EventLog", b =>
                {
                    b.HasOne("BulkyWeb.Models.Student", "Student")
                        .WithMany("EventLogs")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BulkyWeb.Models.ObjectMetric", b =>
                {
                    b.HasOne("BulkyWeb.Models.Student", "Student")
                        .WithMany("ObjectMetrics")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BulkyWeb.Models.Student", b =>
                {
                    b.HasOne("BulkyWeb.Models.Professor", "Professor")
                        .WithMany("Students")
                        .HasForeignKey("ProfessorId");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("BulkyWeb.Models.ObjectMetric", b =>
                {
                    b.Navigation("associatedTaskIndexes");
                });

            modelBuilder.Entity("BulkyWeb.Models.Professor", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("BulkyWeb.Models.Student", b =>
                {
                    b.Navigation("EventLogs");

                    b.Navigation("ObjectMetrics");
                });
#pragma warning restore 612, 618
        }
    }
}
