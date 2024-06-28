using Exceptions;
using RepositoryContracts;
using ServiceContracts.FinnhubService;
using System.Net.Http;
using System.Text.Json;

namespace Services.FinnhubService
{
    public class FinnhubSearchStocksService : IFinnhubSearchStocksService
    {
        IFinnhubRepository _finnhubRepository;

        public FinnhubSearchStocksService  (IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        /// <summary>
        /// Searches for a specific stock symbol using the Finnhub API.
        /// </summary>
        /// <param name="stockSymbolToSearch">The stock symbol to search for.</param>
        /// <returns>
        /// A dictionary representing the search result, containing stock details.
        /// </returns>
        /// <exception cref="FinnhubException">
        /// Thrown when no response is received from the Finnhub server or if the API returns an error.
        /// </exception>
        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            try
            {
                // Invoke _finnhubRepository
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);

                return responseDictionary;
            }
            catch (Exception ex)
            {
                throw new FinnhubException($"Error in {nameof(SearchStocks)}", ex);
            }
        }
    }
}
