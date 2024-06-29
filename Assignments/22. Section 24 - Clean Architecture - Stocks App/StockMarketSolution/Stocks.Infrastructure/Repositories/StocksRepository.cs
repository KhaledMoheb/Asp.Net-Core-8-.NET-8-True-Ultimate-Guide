using Microsoft.EntityFrameworkCore;
using Stocks.Core.Entities.Model;
using Stocks.Core.RepositoryContracts;
using Stocks.Infrastructure.DbContexts;

namespace Stocks.Infrastructure.Repositories
{
    /// <summary>
    /// Service class for managing buy and sell orders.
    /// </summary>
    public class StocksRepository : IStocksRepository
    {
        private StockMarketDbContext _stockMarketDbContext;

        public StocksRepository(StockMarketDbContext stockMarketDbContext) => _stockMarketDbContext = stockMarketDbContext;

        /// <summary>
        /// Inserts a new buy order into the list of buy orders.
        /// </summary>
        /// <param name="buyOrder">The buy order request to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the buy order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when buyOrderRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown when buyOrderRequest validation fails.</exception>
        public async Task<BuyOrder> CreateBuyOrder(BuyOrder? buyOrder)
        {
            // Insert buyOrder into Database
            await _stockMarketDbContext.BuyOrders.AddAsync(buyOrder);
            await _stockMarketDbContext.SaveChangesAsync();

            return await Task.FromResult(buyOrder);
        }

        /// <summary>
        /// Inserts a new sell order into the list of sell orders.
        /// </summary>
        /// <param name="sellOrder">The sell order request to insert.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the sell order response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when sellOrderRequest is null.</exception>
        /// <exception cref="ArgumentException">Thrown when sellOrderRequest validation fails.</exception>
        public async Task<SellOrder> CreateSellOrder(SellOrder? sellOrder)
        {
            // Insert sellOrder into Database
            await _stockMarketDbContext.SellOrders.AddAsync(sellOrder);
            await _stockMarketDbContext.SaveChangesAsync();

            return await Task.FromResult(sellOrder);
        }

        /// <summary>
        /// Retrieves the list of buy orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of buy order responses.</returns>
        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _stockMarketDbContext.BuyOrders
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .ToListAsync();
            return await Task.FromResult(buyOrders);
        }

        /// <summary>
        /// Retrieves the list of sell orders.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of sell order responses.</returns>
        public async Task<List<SellOrder>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _stockMarketDbContext.SellOrders
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .ToListAsync();
            return await Task.FromResult(sellOrders);
        }
    }
}
