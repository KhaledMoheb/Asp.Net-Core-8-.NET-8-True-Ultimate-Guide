using Stocks.Core.Exceptions;
using Stocks.Core.RepositoryContracts;
using Stocks.Core.ServiceContracts.FinnhubService;

namespace Stocks.Core.Services.FinnhubService
{
    public class FinnhubStocksService : IFinnhubStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        /// <summary>
        /// Retrieves a list of stock symbols from the Finnhub API.
        /// </summary>
        /// <returns>
        /// A list of dictionaries, where each dictionary represents a stock symbol and its details.
        /// </returns>
        /// <exception cref="FinnhubException">
        /// Thrown when no response is received from the Finnhub server.
        /// </exception>
        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            try
            {
                // Invoke repository
                List<Dictionary<string, string>>? responseDictionaries = await _finnhubRepository.GetStocks();
                return responseDictionaries;
            }
            catch (Exception ex)
            {
                throw new FinnhubException($"Error in {nameof(GetStocks)}", ex);
            }
        }
    }
}
