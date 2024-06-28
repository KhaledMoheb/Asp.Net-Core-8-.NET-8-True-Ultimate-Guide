using Entity.Model;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksService;
using Services.Helpers;

namespace Services.StocksService
{
    public class StocksSellOrdersService : ISellOrdersService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksSellOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        /// <summary>
        /// Inserts a new sell order into the list of sell orders.
        /// </summary>
        /// <param name="sellOrder">The sell order request to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sell order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sellOrderRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown when sellOrderRequest validation fails.</exception>
        public async Task<SellOrderResponse> CreateSellOrder(SellOrder? sellOrder)
        {
            if (sellOrder == null)
            {
                throw new ArgumentNullException(nameof(sellOrder));
            }

            ValidationHelper.ModelValidation(sellOrder);

            sellOrder.SellOrderID = Guid.NewGuid();

            // Create new sell order throught _stocksRepository
            SellOrder sellOrderFromRepository = await _stocksRepository.CreateSellOrder(sellOrder);

            return await Task.FromResult(sellOrderFromRepository.ToSellOrderResponse());
        }

        /// <summary>
        /// Retrieves a list of existing sell orders.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with the list of sell order responses.</returns>
        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _stocksRepository.GetSellOrders();
            List<SellOrderResponse> sellOrderResponses = sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
            return sellOrderResponses;
        }
    }
}
