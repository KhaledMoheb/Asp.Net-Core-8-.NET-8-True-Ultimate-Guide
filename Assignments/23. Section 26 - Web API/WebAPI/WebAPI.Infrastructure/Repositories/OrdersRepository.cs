using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI.Core.Entities;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Infrastructure.DBContexts;

namespace WebAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on Order entities.
    /// </summary>
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApplicationDBContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersRepository"/> class.
        /// </summary>
        /// <param name="applicationDBContext">The database context to be used for data operations.</param>
        public OrdersRepository(ApplicationDBContext applicationDBContext)
        {
            _db = applicationDBContext;
        }

        /// <summary>
        /// Creates a new Order asynchronously.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order with the generated ID.</returns>
        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.OrderId = Guid.NewGuid();
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order;
        }

        /// <summary>
        /// Deletes an Order by its ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        /// <returns>True if the order was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            Order? order = await _db.Orders.FindAsync(orderId);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves all Orders asynchronously.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order> orders = await _db.Orders.OrderBy(order => order.OrderId).ToListAsync();
            return orders;
        }

        /// <summary>
        /// Retrieves orders that match a specified condition asynchronously.
        /// </summary>
        /// <param name="predicate">The condition to filter orders.</param>
        /// <returns>A list of orders that match the condition.</returns>
        public async Task<List<Order>> GetFilteredOrders(Expression<Func<Order, bool>> predicate)
        {
            List<Order> orders = await _db.Orders.Where(predicate).ToListAsync();
            return orders;
        }

        /// <summary>
        /// Retrieves an Order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            Order? order = await _db.Orders.FindAsync(id);
            return order;
        }

        /// <summary>
        /// Updates an existing Order asynchronously.
        /// </summary>
        /// <param name="order">The order with updated values.</param>
        /// <returns>The updated order.</returns>
        /// <exception cref="ArgumentException">Thrown when the order to update does not exist.</exception>
        public async Task<Order> UpdateOrderAsync(Order order)
        {
            Order? updatedOrder = await _db.Orders.FindAsync(order.OrderId);
            if (updatedOrder == null)
            {
                throw new ArgumentException($"Order {order.OrderId} does not exist");
            }

            updatedOrder.OrderNumber = order.OrderNumber;
            updatedOrder.OrderDate = order.OrderDate;
            updatedOrder.CustomerName = order.CustomerName;
            updatedOrder.TotalAmount = order.TotalAmount;

            await _db.SaveChangesAsync();

            return updatedOrder;
        }
    }
}
