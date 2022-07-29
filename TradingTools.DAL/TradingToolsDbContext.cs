using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingTools.Trunk.Entity;

namespace TradingTools.DAL
{
    public class TradingToolsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString())
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Using keyless entity is not generating migration file - failing on Add-Migration - Null Reference
            //modelBuilder.Entity<TradeThread>()
            //    .HasNoKey();

            /// Explicit Entities Relationship Declaration
            /// 



            /// TradeChallengeProspect Entity Relationship
            /// Explicitly declared to cascade on delete
            modelBuilder.Entity<TradeChallenge>()
                .HasMany(tc => tc.TradeChallengeProspects)
                .WithOne(tcp => tcp.TradeChallenge)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CalculatorState>()
                .HasOne(tc => tc.TradeChallengeProspect)
                .WithOne(tcp => tcp.CalculatorState)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            /// Decimal Place Setting
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                //// EF Core 1 & 2
                //property.Relational().ColumnType = "decimal(18, 6)";

                //EF Core 3
                property.SetColumnType("decimal(18, 6)");
            }
        }

        public DbSet<Trade> Trade { get; set; }
        public DbSet<CalculatorState> CalculatorState { get; set; }
        public DbSet<TradeChallenge> TradeChallenge { get; set; }
        public DbSet<TradeThread> TradeThread { get; set; }
        public DbSet<TradeChallengeProspect> TradeChallengeProspect { get; set; }
    }
}
