using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.Orders;

namespace WebAPI.Core.Services.Orders
{
    /// <summary>
    /// Service class for retrieving order data.
    /// </summary>
    public class OrdersGetterService : IOrdersGetterService
    {
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersGetterService"/> class.
        /// </summary>
        /// <param name="ordersRepository">The repository used for order data operations.</param>
        public OrdersGetterService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Retrieves an order by its unique identifier.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the order response or null if the order does not exist.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderId is an empty GUID.</exception>
        public async Task<OrderResponse?> GetOrderByOrderIdAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            Order order = await _ordersRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return null;
            }

            return order.ToOrderResponse();
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all order responses.</returns>
        public async Task<List<OrderResponse>> GetAllOrdersAsync()
        {
            List<Order> orders = await _ordersRepository.GetAllOrdersAsync();
            List<OrderResponse> orderResponses = orders.Select(order => order.ToOrderResponse()).ToList();
            return orderResponses;
        }
    }
}
