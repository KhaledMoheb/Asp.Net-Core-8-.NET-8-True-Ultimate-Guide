using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.Helpers;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.Orders;

namespace WebAPI.Core.Services.Orders
{
    /// <summary>
    /// Service class for adding new orders.
    /// </summary>
    public class OrdersAdderService : IOrdersAdderService
    {
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersAdderService"/> class.
        /// </summary>
        /// <param name="ordersRepository">The repository used for order data operations.</param>
        public OrdersAdderService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Creates a new order asynchronously.
        /// </summary>
        /// <param name="orderAddRequest">The order add request containing order details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the order add request is null.</exception>
        public async Task<OrderResponse> CreateOrderAsync(OrderAddRequest orderAddRequest)
        {
            if (orderAddRequest == null)
            {
                throw new ArgumentNullException(nameof(orderAddRequest));
            }

            ValidationHelper.ModelValidation(orderAddRequest);

            Order order = await _ordersRepository.CreateOrderAsync(orderAddRequest);

            return order.ToOrderResponse();
        }
    }
}
