﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalPortfolio.Shared.Storage;

namespace PersonalPortfolio.Shared.Storage.Migrations
{
    [DbContext(typeof(PortfolioDbContext))]
    partial class PortfolioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PersonalPortfolio.Shared.Storage.Security", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Securities");
                });

            modelBuilder.Entity("PersonalPortfolio.Shared.Storage.SymbolRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SourceSymbolId")
                        .HasColumnType("int");

                    b.Property<int>("TargetSymbolId")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("SourceSymbolId");

                    b.HasIndex("TargetSymbolId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("PersonalPortfolio.Shared.Storage.SymbolRate", b =>
                {
                    b.HasOne("PersonalPortfolio.Shared.Storage.Security", "SourceSymbol")
                        .WithMany()
                        .HasForeignKey("SourceSymbolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalPortfolio.Shared.Storage.Security", "TargetSymbol")
                        .WithMany()
                        .HasForeignKey("TargetSymbolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}