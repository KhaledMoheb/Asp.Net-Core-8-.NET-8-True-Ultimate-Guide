using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.Orders
{
    /// <summary>
    /// Service interface for retrieving order data.
    /// </summary>
    public interface IOrdersGetterService
    {
        /// <summary>
        /// Retrieves an order by its unique identifier.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the order response or null if the order does not exist.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderId is an empty GUID.</exception>
        public abstract Task<OrderResponse?> GetOrderByOrderIdAsync(Guid orderId);

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all order responses.</returns>
        public abstract Task<List<OrderResponse>> GetAllOrdersAsync();
    }
}
