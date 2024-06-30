using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.Orders
{
    /// <summary>
    /// Service interface for updating order data.
    /// </summary>
    public interface IOrdersUpdaterService
    {
        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderUpdateRequest">The order update request containing the updated order data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderUpdateRequest is null.</exception>
        public abstract Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest orderUpdateRequest);
    }
}
