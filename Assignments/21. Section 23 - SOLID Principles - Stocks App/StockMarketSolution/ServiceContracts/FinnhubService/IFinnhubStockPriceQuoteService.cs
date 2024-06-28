namespace ServiceContracts.FinnhubService
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubStockPriceQuoteService
    {
        /// <summary>
        /// Retrieves stock price quote for the specified stock symbol.
        /// </summary>
        /// <param name="stockSymbol">Symbol of the stock to retrieve price quote for.</param>
        /// <returns>A task representing the asynchronous operation with a dictionary containing price quote details.</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
