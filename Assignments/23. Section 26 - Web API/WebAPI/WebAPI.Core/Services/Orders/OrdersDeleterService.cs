using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.Orders;

namespace WebAPI.Core.Services.Orders
{
    /// <summary>
    /// Service class for deleting orders.
    /// </summary>
    public class OrdersDeleterService : IOrdersDeleterService
    {
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersDeleterService"/> class.
        /// </summary>
        /// <param name="ordersRepository">The repository used for order data operations.</param>
        public OrdersDeleterService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Deletes an order asynchronously.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the order was successfully deleted.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderId is an empty GUID.</exception>
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            bool deletionFlag = await _ordersRepository.DeleteOrderAsync(orderId);

            return deletionFlag;
        }
    }
}
