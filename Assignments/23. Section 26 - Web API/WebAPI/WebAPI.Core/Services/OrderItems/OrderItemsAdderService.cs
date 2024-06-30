using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.Helpers;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.OrderItems;

namespace WebAPI.Core.Services.OrderItems
{
    /// <summary>
    /// Service class for adding new order items.
    /// </summary>
    public class OrderItemsAdderService : IOrderItemsAdderService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemsAdderService"/> class.
        /// </summary>
        /// <param name="orderItemsRepository">The repository used for order item data operations.</param>
        public OrderItemsAdderService(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        /// <summary>
        /// Creates a new order item.
        /// </summary>
        /// <param name="orderItemAddRequest">The order item add request containing the order item data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created order item response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderItemAddRequest is null.</exception>
        public async Task<OrderItemResponse> CreateOrderItemAsync(OrderItemAddRequest orderItemAddRequest)
        {
            if (orderItemAddRequest == null)
            {
                throw new ArgumentNullException(nameof(orderItemAddRequest));
            }

            ValidationHelper.ModelValidation(orderItemAddRequest);

            OrderItem orderItem = await _orderItemsRepository.CreateOrderItemAsync(orderItemAddRequest.ToOrderItem());

            return orderItem.ToOrderItemResponse();
        }
    }
}
