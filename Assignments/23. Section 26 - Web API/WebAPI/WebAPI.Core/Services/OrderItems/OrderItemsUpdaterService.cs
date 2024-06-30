using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.Helpers;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.OrderItems;

namespace WebAPI.Core.Services.OrderItems
{
    /// <summary>
    /// Service class for updating order items.
    /// </summary>
    public class OrderItemsUpdaterService : IOrderItemsUpdaterService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemsUpdaterService"/> class.
        /// </summary>
        /// <param name="orderItemsRepository">The repository used for order item data operations.</param>
        public OrderItemsUpdaterService(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        /// <summary>
        /// Updates an order item based on the provided update request.
        /// </summary>
        /// <param name="orderItemUpdateRequest">The update request containing new order item data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="OrderItemResponse"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided orderItemUpdateRequest is null.</exception>
        public async Task<OrderItemResponse> UpdateOrderItemAsync(OrderItemUpdateRequest orderItemUpdateRequest)
        {
            if (orderItemUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(orderItemUpdateRequest));
            }

            ValidationHelper.ModelValidation(orderItemUpdateRequest);

            OrderItem orderItem = await _orderItemsRepository.UpdateOrderItemAsync(orderItemUpdateRequest.ToOrderItem());

            return orderItem.ToOrderItemResponse();
        }
    }
}
