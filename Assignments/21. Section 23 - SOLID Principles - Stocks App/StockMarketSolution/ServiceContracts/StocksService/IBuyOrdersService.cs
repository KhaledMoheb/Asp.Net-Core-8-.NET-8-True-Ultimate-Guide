using Entity.Model;
using ServiceContracts.DTO;

namespace ServiceContracts.StocksService
{
    /// <summary>
    /// Represents Stocks service that includes buy order operations.
    /// </summary>
    public interface IBuyOrdersService
    {
        /// <summary>
        /// Creates a new buy order.
        /// </summary>
        /// <param name="buyOrder">Request object containing buy order details.</param>
        /// <returns>A task representing the asynchronous operation with the response of the created buy order.</returns>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrder? buyOrder);

        /// <summary>
        /// Retrieves a list of existing buy orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of buy order responses.</returns>
        Task<List<BuyOrderResponse>> GetBuyOrders();
    }
}
