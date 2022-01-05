﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.KYC.Turnover.Postgres;

#nullable disable

namespace Service.KYC.Turnover.Postgres.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("kycturnover")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Service.Bitgo.DepositDetector.Domain.Models.Deposit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("AssetSymbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BrokerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CardLast4")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("FeeAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("FeeAssetSymbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Integration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastError")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MatchingEngineId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Network")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RetriesCount")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Txid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("WalletId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("WalletId");

                    b.ToTable("deposits", "kycturnover");
                });

            modelBuilder.Entity("Service.KYC.Turnover.Domain.Models.TurnoverSettings", b =>
                {
                    b.Property<string>("SettingsKey")
                        .HasColumnType("text");

                    b.Property<string>("LimitAsset")
                        .HasColumnType("text");

                    b.Property<decimal>("LimitLevel2")
                        .HasColumnType("numeric");

                    b.Property<decimal>("LimitLevel3")
                        .HasColumnType("numeric");

                    b.HasKey("SettingsKey");

                    b.ToTable("settings", "kycturnover");
                });
#pragma warning restore 612, 618
        }
    }
}
