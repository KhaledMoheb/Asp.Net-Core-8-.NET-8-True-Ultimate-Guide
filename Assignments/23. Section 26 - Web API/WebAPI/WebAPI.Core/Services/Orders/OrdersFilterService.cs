using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.Orders;

namespace WebAPI.Core.Services.Orders
{
    /// <summary>
    /// Service class for filtering orders based on various criteria.
    /// </summary>
    public class OrdersFilterService : IOrdersFilterService
    {
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersFilterService"/> class.
        /// </summary>
        /// <param name="ordersRepository">The repository used for order data operations.</param>
        public OrdersFilterService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Filters orders based on the specified criteria.
        /// </summary>
        /// <param name="searchBy">The property to search by (e.g., CustomerName, OrderDate, OrderNumber).</param>
        /// <param name="searchString">The search string used to filter orders.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of filtered order responses.</returns>
        public async Task<List<OrderResponse>> GetFilteredOrdersAsync(string searchBy, string? searchString)
        {
            List<Order> filteredOrders = new List<Order>();

            switch (searchBy)
            {
                case nameof(Order.CustomerName):
                    filteredOrders = await _ordersRepository.GetFilteredOrders(order => order.CustomerName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                    break;
                case nameof(Order.OrderDate):
                    filteredOrders = await _ordersRepository.GetFilteredOrders(order => order.OrderDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase));
                    break;
                case nameof(Order.OrderNumber):
                    filteredOrders = await _ordersRepository.GetFilteredOrders(order => order.OrderNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    throw new ArgumentException("Invalid search criteria", nameof(searchBy));
            }

            List<OrderResponse> orderResponses = filteredOrders.Select(order => order.ToOrderResponse()).ToList();
            return orderResponses;
        }
    }
}
