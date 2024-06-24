namespace ServiceContract
{
    /// <summary>
    /// Interface for retrieving stock price quotes using Finnhub API.
    /// </summary>
    public interface IFinnhubStockPriceQuoteService : IFinnhubService
    {
        /// <summary>
        /// Retrieves stock price quote for the specified stock symbol.
        /// </summary>
        /// <param name="stockSymbol">Symbol of the stock to retrieve price quote for.</param>
        /// <returns>A task representing the asynchronous operation with a dictionary containing price quote details.</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
