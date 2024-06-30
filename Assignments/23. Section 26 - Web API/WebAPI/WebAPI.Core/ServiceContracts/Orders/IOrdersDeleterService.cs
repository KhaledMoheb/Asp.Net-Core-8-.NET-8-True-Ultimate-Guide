namespace WebAPI.Core.ServiceContracts.Orders
{
    /// <summary>
    /// Service interface for deleting orders.
    /// </summary>
    public interface IOrdersDeleterService
    {
        /// <summary>
        /// Deletes an order asynchronously.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the order was successfully deleted.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the orderId is an empty GUID.</exception>
        public abstract Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
