namespace Stocks.Core.ServiceContracts.FinnhubService
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubSearchStocksService
    {
        /// <summary>
        /// Searches for a specific stock symbol using the Finnhub API.
        /// </summary>
        /// <param name="stockSymbolToSearch">The stock symbol to search for.</param>
        /// <returns>
        /// A dictionary representing the search result, containing stock details.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no response is received from the Finnhub server or if the API returns an error.
        /// </exception>
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
