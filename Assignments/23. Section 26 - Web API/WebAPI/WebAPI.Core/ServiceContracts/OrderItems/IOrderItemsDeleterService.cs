namespace WebAPI.Core.ServiceContracts.OrderItems
{
    /// <summary>
    /// Service interface for deleting order items.
    /// </summary>
    public interface IOrderItemsDeleterService
    {
        /// <summary>
        /// Deletes an order item by its unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier of the order item to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided guid is empty.</exception>
        public abstract Task<bool> DeleteOrderItemAsync(Guid guid);
    }
}
