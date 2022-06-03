﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollAPI.Models;

#nullable disable

namespace PayrollAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PayrollAPI.Models.OverTime", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime>("endAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isSalaryCalculated")
                        .HasColumnType("bit");

                    b.Property<int>("staffId")
                        .HasColumnType("int");

                    b.Property<DateTime>("startAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("staffId");

                    b.ToTable("OverTimes");
                });

            modelBuilder.Entity("PayrollAPI.Models.Salary", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<float>("insurance")
                        .HasColumnType("real");

                    b.Property<bool>("isDelivered")
                        .HasColumnType("bit");

                    b.Property<string>("month")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<float>("salaryBasic")
                        .HasColumnType("real");

                    b.Property<float>("salaryOT")
                        .HasColumnType("real");

                    b.Property<float>("salaryReceived")
                        .HasColumnType("real");

                    b.Property<int>("staffId")
                        .HasColumnType("int");

                    b.Property<float>("tax")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("staffId");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("PayrollAPI.Models.Staff", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("salary")
                        .HasColumnType("real");

                    b.Property<string>("sex")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("PayrollAPI.Models.OverTime", b =>
                {
                    b.HasOne("PayrollAPI.Models.Staff", "staff")
                        .WithMany("overTimes")
                        .HasForeignKey("staffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("staff");
                });

            modelBuilder.Entity("PayrollAPI.Models.Salary", b =>
                {
                    b.HasOne("PayrollAPI.Models.Staff", "staff")
                        .WithMany("salaries")
                        .HasForeignKey("staffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("staff");
                });

            modelBuilder.Entity("PayrollAPI.Models.Staff", b =>
                {
                    b.Navigation("overTimes");

                    b.Navigation("salaries");
                });
#pragma warning restore 612, 618
        }
    }
}
