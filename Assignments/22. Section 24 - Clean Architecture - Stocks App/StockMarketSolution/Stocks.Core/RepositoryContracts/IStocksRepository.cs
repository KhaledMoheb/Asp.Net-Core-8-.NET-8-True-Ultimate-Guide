using Stocks.Core.Entities.Model;

namespace Stocks.Core.RepositoryContracts
{
    /// <summary>
    /// Interface for managing stock orders.
    /// </summary>
    public interface IStocksRepository
    {
        /// <summary>
        /// Creates a new buy order.
        /// </summary>
        /// <param name="buyOrder">Request object containing buy order details.</param>
        /// <returns>A task representing the asynchronous operation with the response of the created buy order.</returns>
        Task<BuyOrder> CreateBuyOrder(BuyOrder? buyOrder);

        /// <summary>
        /// Creates a new sell order.
        /// </summary>
        /// <param name="sellOrder">Request object containing sell order details.</param>
        /// <returns>A task representing the asynchronous operation with the response of the created sell order.</returns>
        Task<SellOrder> CreateSellOrder(SellOrder? sellOrder);

        /// <summary>
        /// Retrieves a list of existing buy orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of buy order responses.</returns>
        Task<List<BuyOrder>> GetBuyOrders();

        /// <summary>
        /// Retrieves a list of existing sell orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of sell order responses.</returns>
        Task<List<SellOrder>> GetSellOrders();
    }
}
