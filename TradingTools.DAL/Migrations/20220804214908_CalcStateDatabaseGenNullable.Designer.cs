﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradingTools.Repository;

namespace TradingTools.Repository.Migrations
{
    [DbContext(typeof(TradingToolsDbContext))]
    [Migration("20220804214908_CalcStateDatabaseGenNullable")]
    partial class CalcStateDatabaseGenNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TradingTools.Trunk.Entity.CalculatorState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("BorrowAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Capital")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("ClosingTradingCost")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("ClosingTradingFee")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("CounterBias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DailyInterestRate")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("DayCount")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("EntryPriceAvg")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("ExchangeFee")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("InterestCost")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<bool>("IsLotSizeChecked")
                        .HasColumnType("bit");

                    b.Property<decimal?>("LEP_ExitPrice")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("LEP_Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Leverage")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("LeveragedCapital")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("LotSize")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("OpeningTradingCost")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("OpeningTradingFee")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("PEP_ExitPrice")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("PEP_Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PerfectEntry_EntryPrice")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("PerfectEntry_ExitPrice")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("PerfectEntry_Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PriceDecreaseTarget")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("PriceIncreaseTarget")
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("ReasonForEntry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReasonForExit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Side")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ticker")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("TradeExit_ExitPrice")
                        .HasColumnType("decimal(18,6)");

                    b.Property<int?>("TradeId")
                        .HasColumnType("int");

                    b.Property<string>("TradingStyle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("TradeId")
                        .IsUnique()
                        .HasFilter("[TradeId] IS NOT NULL");

                    b.ToTable("CalculatorState");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("BorrowAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("Capital")
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("DateEnter")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateExit")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("DayCount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("EntryPriceAvg")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("ExitPriceAvg")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("FinalCapital")
                        .HasColumnType("decimal(18,6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Leverage")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("LeveragedCapital")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("LotSize")
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("PnL")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal?>("PnL_percentage")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("decimal(18,6)");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TradingStyle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.TradeChallenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TradeCap")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TradeChallenge");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.TradeChallengeProspect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalculatorStateId")
                        .HasColumnType("int");

                    b.Property<int>("TradeChallengeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CalculatorStateId")
                        .IsUnique();

                    b.HasIndex("TradeChallengeId");

                    b.ToTable("TradeChallengeProspect");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.TradeThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TradeChallengeId")
                        .HasColumnType("int");

                    b.Property<int?>("TradeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TradeChallengeId");

                    b.HasIndex("TradeId")
                        .IsUnique()
                        .HasFilter("[TradeId] IS NOT NULL");

                    b.ToTable("TradeThread");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.CalculatorState", b =>
                {
                    b.HasOne("TradingTools.Trunk.Entity.Trade", "Trade")
                        .WithOne("CalculatorState")
                        .HasForeignKey("TradingTools.Trunk.Entity.CalculatorState", "TradeId");

                    b.Navigation("Trade");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.TradeChallengeProspect", b =>
                {
                    b.HasOne("TradingTools.Trunk.Entity.CalculatorState", "CalculatorState")
                        .WithOne("TradeChallengeProspect")
                        .HasForeignKey("TradingTools.Trunk.Entity.TradeChallengeProspect", "CalculatorStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TradingTools.Trunk.Entity.TradeChallenge", "TradeChallenge")
                        .WithMany("TradeChallengeProspects")
                        .HasForeignKey("TradeChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalculatorState");

                    b.Navigation("TradeChallenge");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.TradeThread", b =>
                {
                    b.HasOne("TradingTools.Trunk.Entity.TradeChallenge", "TradeChallenge")
                        .WithMany("TradeThreads")
                        .HasForeignKey("TradeChallengeId");

                    b.HasOne("TradingTools.Trunk.Entity.Trade", "Trade")
                        .WithOne("TradeThread")
                        .HasForeignKey("TradingTools.Trunk.Entity.TradeThread", "TradeId");

                    b.Navigation("Trade");

                    b.Navigation("TradeChallenge");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.CalculatorState", b =>
                {
                    b.Navigation("TradeChallengeProspect");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.Trade", b =>
                {
                    b.Navigation("CalculatorState");

                    b.Navigation("TradeThread");
                });

            modelBuilder.Entity("TradingTools.Trunk.Entity.TradeChallenge", b =>
                {
                    b.Navigation("TradeChallengeProspects");

                    b.Navigation("TradeThreads");
                });
#pragma warning restore 612, 618
        }
    }
}
