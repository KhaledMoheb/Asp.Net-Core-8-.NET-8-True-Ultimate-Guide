using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.Orders
{
    /// <summary>
    /// Service interface for adding new orders.
    /// </summary>
    public interface IOrdersAdderService
    {
        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="orderAddRequest">The order add request containing order details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the order add request is null.</exception>
        public abstract Task<OrderResponse> CreateOrderAsync(OrderAddRequest orderAddRequest);
    }
}
