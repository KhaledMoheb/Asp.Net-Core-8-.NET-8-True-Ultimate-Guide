using Stocks.Core.Exceptions;
using Stocks.Core.RepositoryContracts;
using Stocks.Core.ServiceContracts.FinnhubService;

namespace Stocks.Core.Services.FinnhubService
{
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        IFinnhubRepository _finnhubRepository;

        public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        /// <summary>
        /// Retrieves stock price quote from Finnhub API.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol for which to fetch the price quote.</param>
        /// <returns>A dictionary containing the response from Finnhub API.</returns>
        /// <exception cref="FinnhubException">Thrown when no response is received from Finnhub server or when there is an error in the response.</exception>
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            try
            {
                // Invoke repository
                Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);
                return responseDictionary;
            }
            catch (Exception ex)
            {
                throw new FinnhubException($"Error in {nameof(GetStockPriceQuote)}", ex);
            }
        }
    }
}
