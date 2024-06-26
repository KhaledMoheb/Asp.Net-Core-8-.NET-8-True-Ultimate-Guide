namespace ServiceContract
{
    /// <summary>
    /// Interface for services requiring access to the Finnhub API token.
    /// </summary>
    public interface IFinnhubService
    {
        /// <summary>
        /// Gets or sets the Finnhub API token.
        /// </summary>
        string _finnhubToken { get; set; }

        /// <summary>
        /// Retrieves company profile information for the specified stock symbol.
        /// </summary>
        /// <param name="stockSymbol">Symbol of the stock to retrieve company profile information for.</param>
        /// <returns>A task representing the asynchronous operation with a dictionary containing company profile details.</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        /// <summary>
        /// Retrieves stock price quote for the specified stock symbol.
        /// </summary>
        /// <param name="stockSymbol">Symbol of the stock to retrieve price quote for.</param>
        /// <returns>A task representing the asynchronous operation with a dictionary containing price quote details.</returns>
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
