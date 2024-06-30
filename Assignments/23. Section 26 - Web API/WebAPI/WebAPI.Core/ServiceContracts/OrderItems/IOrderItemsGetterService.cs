using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.OrderItems
{
    /// <summary>
    /// Service interface for retrieving order items.
    /// </summary>
    public interface IOrderItemsGetterService
    {
        /// <summary>
        /// Retrieves all order items.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrderItemResponse"/> objects.</returns>
        public abstract Task<List<OrderItemResponse>> GetAllOrderItemsAsync();
        
        /// <summary>
        /// Retrieves an order item by its unique identifier.
        /// </summary>
        /// <param name="orderItemId">The unique identifier of the order item.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OrderItemResponse"/> object or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided orderItemId is empty.</exception>
        public abstract Task<List<OrderItemResponse>> GetOrderItemsOfOrderIdAsync(Guid orderId);

        /// <summary>
        /// Retrieves order items by the order identifier they belong to.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrderItemResponse"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided orderId is empty.</exception>
        public abstract Task<OrderItemResponse?> GetOrderItemByOrderItemIdAsync(Guid orderItemId);
    }
}
