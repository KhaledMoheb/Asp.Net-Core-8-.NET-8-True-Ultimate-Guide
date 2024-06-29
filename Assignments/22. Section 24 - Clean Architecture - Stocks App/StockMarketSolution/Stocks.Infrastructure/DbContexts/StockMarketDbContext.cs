using Microsoft.EntityFrameworkCore;
using Stocks.Core.Entities.Model;

namespace Stocks.Infrastructure.DbContexts
{
    /// <summary>
    /// Represents the database context for the Stock Market application.
    /// </summary>
    public class StockMarketDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockMarketDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public StockMarketDbContext(DbContextOptions<StockMarketDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for Buy Orders.
        /// </summary>
        public DbSet<BuyOrder> BuyOrders { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Sell Orders.
        /// </summary>
        public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
        }
    }
}
