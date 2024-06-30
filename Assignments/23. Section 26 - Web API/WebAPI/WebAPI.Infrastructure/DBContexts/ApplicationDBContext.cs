using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Entities;

namespace WebAPI.Infrastructure.DBContexts
{
    public class ApplicationDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDBContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the database context.</param>
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for Orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Order Items.
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
            .Property(orderItem => orderItem.TotalPrice)
            .HasComputedColumnSql($"{nameof(OrderItem.Quantity)} * {nameof(OrderItem.UnitPrice)}");

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
        }

    }
}
