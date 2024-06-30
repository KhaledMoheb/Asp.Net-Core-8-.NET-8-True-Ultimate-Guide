using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.OrderItems
{
    /// <summary>
    /// Service interface for adding new order items.
    /// </summary>
    public interface IOrderItemsAdderService
    {
        /// <summary>
        /// Creates a new order item.
        /// </summary>
        /// <param name="orderItemAddRequest">The order item add request containing the order item data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created order item response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderItemAddRequest is null.</exception>
        public abstract Task<OrderItemResponse> CreateOrderItemAsync(OrderItemAddRequest orderItemAddRequest);
    }
}
