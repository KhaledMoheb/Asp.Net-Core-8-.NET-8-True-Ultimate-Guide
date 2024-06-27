using Entity.DTO;

namespace ServiceContract
{
    /// <summary>
    /// Interface for managing stock orders.
    /// </summary>
    public interface IStocksService
    {
        /// <summary>
        /// Creates a new buy order.
        /// </summary>
        /// <param name="buyOrderRequest">Request object containing buy order details.</param>
        /// <returns>A task representing the asynchronous operation with the response of the created buy order.</returns>
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        /// <summary>
        /// Creates a new sell order.
        /// </summary>
        /// <param name="sellOrderRequest">Request object containing sell order details.</param>
        /// <returns>A task representing the asynchronous operation with the response of the created sell order.</returns>
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        /// <summary>
        /// Retrieves a list of existing buy orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of buy order responses.</returns>
        Task<List<BuyOrderResponse>> GetBuyOrders();

        /// <summary>
        /// Retrieves a list of existing sell orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of sell order responses.</returns>
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
