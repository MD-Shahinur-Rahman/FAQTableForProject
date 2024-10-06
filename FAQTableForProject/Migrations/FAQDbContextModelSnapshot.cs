﻿// <auto-generated />
using System;
using FAQTableForProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FAQTableForProject.Migrations
{
    [DbContext(typeof(FAQDbContext))]
    partial class FAQDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FAQTableForProject.Models.FAQ", b =>
                {
                    b.Property<int>("FAQId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FAQId"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FAQCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FAQId");

                    b.HasIndex("FAQCategoryId");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("FAQTableForProject.Models.FAQCategory", b =>
                {
                    b.Property<int>("FAQCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FAQCategoryId"));

                    b.Property<string>("FAQCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FAQCategoryId");

                    b.ToTable("FAQCategory");
                });

            modelBuilder.Entity("FAQTableForProject.Models.Partner", b =>
                {
                    b.Property<int>("PartnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartnerId"));

                    b.Property<DateTime>("AgreementEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AgreementSignDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AgreementTotal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("PartnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostCode")
                        .HasColumnType("int");

                    b.HasKey("PartnerId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("Term", b =>
                {
                    b.Property<int>("TermId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TermId"));

                    b.Property<int>("PartnerId")
                        .HasColumnType("int");

                    b.Property<string>("TermDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TermId");

                    b.HasIndex("PartnerId");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("FAQTableForProject.Models.FAQ", b =>
                {
                    b.HasOne("FAQTableForProject.Models.FAQCategory", "FAQCategory")
                        .WithMany("FAQs")
                        .HasForeignKey("FAQCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FAQCategory");
                });

            modelBuilder.Entity("Term", b =>
                {
                    b.HasOne("FAQTableForProject.Models.Partner", "Partner")
                        .WithMany("Terms")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("FAQTableForProject.Models.FAQCategory", b =>
                {
                    b.Navigation("FAQs");
                });

            modelBuilder.Entity("FAQTableForProject.Models.Partner", b =>
                {
                    b.Navigation("Terms");
                });
#pragma warning restore 612, 618
        }
    }
}
