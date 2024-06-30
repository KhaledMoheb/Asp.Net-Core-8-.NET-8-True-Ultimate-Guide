using WebAPI.Core.RepositoryContracts;
using WebAPI.Core.ServiceContracts.OrderItems;

namespace WebAPI.Core.Services.OrderItems
{
    /// <summary>
    /// Service class for deleting order items.
    /// </summary>
    public class OrderItemsDeleterService : IOrderItemsDeleterService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemsDeleterService"/> class.
        /// </summary>
        /// <param name="orderItemsRepository">The repository used for order item data operations.</param>
        public OrderItemsDeleterService(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        /// <summary>
        /// Deletes an order item by its unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier of the order item to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided guid is empty.</exception>
        public async Task<bool> DeleteOrderItemAsync(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(guid));
            }

            bool deletionFlag = await _orderItemsRepository.DeleteOrderItemAsync(guid);

            return deletionFlag;
        }
    }
}
