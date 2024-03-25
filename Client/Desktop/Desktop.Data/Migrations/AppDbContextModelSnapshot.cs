﻿// <auto-generated />
using System;
using Bell.Reconciliation.Frontend.Desktop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Desktop.Data.Migrations
{
    [DbContext(typeof(StapleSourceContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Bell.Reconciliation.Common.Models.BellSourceDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Imei")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lob")
                        .HasColumnType("TEXT");

                    b.Property<int>("MatchStatus")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RebateType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReconciledBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("SubLob")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BellSources");
                });

            modelBuilder.Entity("Bell.Reconciliation.Common.Models.StaplesSourceDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeviceCo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Imei")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<int>("MatchStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Msf")
                        .HasColumnType("TEXT");

                    b.Property<long?>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Product")
                        .HasColumnType("TEXT");

                    b.Property<string>("RebateType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rec")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReconciledBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("SalesPerson")
                        .HasColumnType("TEXT");

                    b.Property<string>("SubLob")
                        .HasColumnType("TEXT");

                    b.Property<long?>("TaxCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StaplesSources");
                });
#pragma warning restore 612, 618
        }
    }
}
