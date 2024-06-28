namespace RepositoryContracts
{
    /// <summary>
    /// Interface for repository requiring access to the Finnhub API token.
    /// </summary>
    public interface IFinnhubRepository
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
