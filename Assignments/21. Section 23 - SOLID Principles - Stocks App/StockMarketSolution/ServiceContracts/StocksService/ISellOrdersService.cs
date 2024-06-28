using Entity.Model;
using ServiceContracts.DTO;

namespace ServiceContracts.StocksService
{
    public interface ISellOrdersService
    {
        /// <summary>
        /// Creates a new sell order.
        /// </summary>
        /// <param name="sellOrder">Request object containing sell order details.</param>
        /// <returns>A task representing the asynchronous operation with the response of the created sell order.</returns>
        Task<SellOrderResponse> CreateSellOrder(SellOrder? sellOrder);

        /// <summary>
        /// Retrieves a list of existing sell orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of sell order responses.</returns>
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
