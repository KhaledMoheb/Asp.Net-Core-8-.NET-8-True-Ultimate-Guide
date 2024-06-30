using WebAPI.Core.Entities;

namespace WebAPI.Core.RepositoryContracts
{
    /// <summary>
    /// Repository class for performing CRUD operations on OrderItem entities.
    /// </summary>
    public interface IOrderItemsRepository
    {
        /// <summary>
        /// Creates a new OrderItem asynchronously.
        /// </summary>
        /// <param name="orderItem">The order item to create.</param>
        /// <returns>The created order item with the generated ID.</returns>
        public abstract Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);

        /// <summary>
        /// Deletes an OrderItem by its ID asynchronously.
        /// </summary>
        /// <param name="orderItemId">The ID of the order item to delete.</param>
        /// <returns>True if the order item was deleted; otherwise, false.</returns>
        public abstract Task<bool> DeleteOrderItemAsync(Guid orderItemId);

        /// <summary>
        /// Retrieves all OrderItems asynchronously.
        /// </summary>
        /// <returns>A list of all order items.</returns>
        public abstract Task<List<OrderItem>> GetAllOrderItems();

        /// <summary>
        /// Retrieves an OrderItem by its ID asynchronously.
        /// </summary>
        /// <param name="orderItemId">The ID of the order item to retrieve.</param>
        /// <returns>The order item with the specified ID, or null if not found.</returns>
        public abstract Task<OrderItem?> GetOrderItemByOrderItemIdAsync(Guid orderItemId);

        /// <summary>
        /// Retrieves all OrderItems for a specific Order ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve items for.</param>
        /// <returns>A list of order items for the specified order ID.</returns>
        public abstract Task<List<OrderItem>> GetOrderItemsOfOrderIdAsync(Guid orderId);

        /// <summary>
        /// Updates an existing OrderItem asynchronously.
        /// </summary>
        /// <param name="orderItem">The order item with updated values.</param>
        /// <returns>The updated order item.</returns>
        /// <exception cref="ArgumentException">Thrown when the order item to update does not exist.</exception>
        public abstract Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem);
    }
}
