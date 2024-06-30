using WebAPI.Core.DTO;

namespace WebAPI.Core.ServiceContracts.Orders
{
    /// <summary>
    /// Service interface for filtering orders based on various criteria.
    /// </summary>
    public interface IOrdersFilterService
    {

        /// <summary>
        /// Filters orders based on the specified criteria.
        /// </summary>
        /// <param name="searchBy">The property to search by (e.g., CustomerName, OrderDate, OrderNumber).</param>
        /// <param name="searchString">The search string used to filter orders.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of filtered order responses.</returns>
        public abstract Task<List<OrderResponse>> GetFilteredOrdersAsync(string searchBy, string? searchString);
    }
}
