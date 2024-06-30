using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.Helpers;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.Orders;

namespace WebAPI.Core.Services.Orders
{
    /// <summary>
    /// Service class for updating order data.
    /// </summary>
    public class OrdersUpdaterService : IOrdersUpdaterService
    {
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersUpdaterService"/> class.
        /// </summary>
        /// <param name="ordersRepository">The repository used for order data operations.</param>
        public OrdersUpdaterService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderUpdateRequest">The order update request containing the updated order data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderUpdateRequest is null.</exception>
        public async Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(orderUpdateRequest));
            }

            ValidationHelper.ModelValidation(orderUpdateRequest);

            Order order = await _ordersRepository.UpdateOrderAsync(orderUpdateRequest);

            return order.ToOrderResponse();
        }
    }
}
