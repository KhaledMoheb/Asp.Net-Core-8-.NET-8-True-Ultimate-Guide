namespace Stocks.Core.ServiceContracts.FinnhubService
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubStocksService
    {
        /// <summary>
        /// Retrieves a list of stock symbols from the Finnhub API.
        /// </summary>
        /// <returns>
        /// A list of dictionaries, where each dictionary represents a stock symbol and its details.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no response is received from the Finnhub server.
        /// </exception>
        Task<List<Dictionary<string, string>>?> GetStocks();
    }
}
