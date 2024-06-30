using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.OrderItems
{
    /// <summary>
    /// Service interface for updating order items.
    /// </summary>
    public interface IOrderItemsUpdaterService
    {
        /// <summary>
        /// Updates an order item based on the provided update request.
        /// </summary>
        /// <param name="orderItemUpdateRequest">The update request containing new order item data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="OrderItemResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided orderItemUpdateRequest is null.</exception>
        public abstract Task<OrderItemResponse> UpdateOrderItemAsync(OrderItemUpdateRequest orderItemUpdateRequest);
    }
}
