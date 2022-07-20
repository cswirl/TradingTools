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
            /// Trade and TradeThread relationship is a little complex than normal - please see the documentation diagram
            /// - A TradeThread record has 2 distinct Trade Record (Head and Tail)
            /// - A Trade record belongs to 1-or-2 Trade Thread records where it is either a head or a tail
            modelBuilder.Entity<Trade>()
                .HasOne<TradeThread>(t => t.TradeThreadHead)
                .WithOne(tread => tread.Trade_head)
                .HasForeignKey<TradeThread>(tread => tread.TradeId_head)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Trade>()
                .HasOne<TradeThread>(t => t.TradeThreadTail)
                .WithOne(tread => tread.Trade_tail)
                .HasForeignKey<TradeThread>(tread => tread.TradeId_tail)
                .OnDelete(DeleteBehavior.NoAction);

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

        public DbSet<Trade> Trades { get; set; }
        public DbSet<CalculatorState> CalculatorStates { get; set; }
    }
}
