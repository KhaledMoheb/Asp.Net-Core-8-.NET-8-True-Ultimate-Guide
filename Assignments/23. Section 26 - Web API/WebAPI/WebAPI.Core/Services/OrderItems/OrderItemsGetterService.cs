using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.OrderItems;

namespace WebAPI.Core.Services.OrderItems
{
    /// <summary>
    /// Service class for retrieving order items.
    /// </summary>
    public class OrderItemsGetterService : IOrderItemsGetterService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemsGetterService"/> class.
        /// </summary>
        /// <param name="orderItemsRepository">The repository used for order item data operations.</param>
        public OrderItemsGetterService(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        /// <summary>
        /// Retrieves all order items.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrderItemResponse"/> objects.</returns>
        public async Task<List<OrderItemResponse>> GetAllOrderItemsAsync()
        {
            List<OrderItem> orderItems = await _orderItemsRepository.GetAllOrderItems();
            List<OrderItemResponse> orderItemResponses = orderItems.Select(orderItem => orderItem.ToOrderItemResponse()).ToList();
            return orderItemResponses;
        }

        /// <summary>
        /// Retrieves an order item by its unique identifier.
        /// </summary>
        /// <param name="orderItemId">The unique identifier of the order item.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="OrderItemResponse"/> object or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided orderItemId is empty.</exception>
        public async Task<OrderItemResponse?> GetOrderItemByOrderItemIdAsync(Guid orderItemId)
        {
            if (orderItemId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderItemId));
            }

            OrderItem orderItem = await _orderItemsRepository.GetOrderItemByOrderItemIdAsync(orderItemId);
            return orderItem?.ToOrderItemResponse();
        }

        /// <summary>
        /// Retrieves order items by the order identifier they belong to.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="OrderItemResponse"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided orderId is empty.</exception>
        public async Task<List<OrderItemResponse>> GetOrderItemsOfOrderIdAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            List<OrderItem> orderItems = await _orderItemsRepository.GetOrderItemsOfOrderIdAsync(orderId);
            List<OrderItemResponse> orderItemResponses = orderItems.Select(orderItem => orderItem.ToOrderItemResponse()).ToList();
            return orderItemResponses;
        }
    }
}
