using System.Linq.Expressions;
using WebAPI.Core.Entities;

namespace WebAPI.Core.RepositoryContracts
{
    /// <summary>
    /// Repository interface for performing CRUD operations on Order entities.
    /// </summary>
    public interface IOrdersRepository
    {
        /// <summary>
        /// Creates a new Order asynchronously.
        /// </summary>
        /// <param name="order">The order to create.</param>
        /// <returns>The created order with the generated ID.</returns>
        public abstract Task<Order> CreateOrderAsync(Order order);

        /// <summary>
        /// Deletes an Order by its ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        /// <returns>True if the order was deleted; otherwise, false.</returns>
        public abstract Task<bool> DeleteOrderAsync(Guid orderId);

        /// <summary>
        /// Retrieves all Orders asynchronously.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        public abstract Task<List<Order>> GetAllOrdersAsync();

        /// <summary>
        /// Retrieves orders that match a specified condition asynchronously.
        /// </summary>
        /// <param name="predicate">The condition to filter orders.</param>
        /// <returns>A list of orders that match the condition.</returns>
        public abstract Task<List<Order>> GetFilteredOrders(Expression<Func<Order, bool>> predicate);

        /// <summary>
        /// Retrieves an Order by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The order with the specified ID, or null if not found.</returns>
        public abstract Task<Order?> GetOrderByIdAsync(Guid id);
        
        /// <summary>
        /// Updates an existing Order asynchronously.
        /// </summary>
        /// <param name="order">The order with updated values.</param>
        /// <returns>The updated order.</returns>
        /// <exception cref="ArgumentException">Thrown when the order to update does not exist.</exception>
        public abstract Task<Order> UpdateOrderAsync(Order order);
    }
}
