namespace Stocks.Core.ServiceContracts.FinnhubService
{
    /// <summary>
    /// Represents a service that makes HTTP requests to finnhub.io
    /// </summary>
    public interface IFinnhubCompanyProfileService
    {
        /// <summary>
        /// Retrieves company profile information for the specified stock symbol.
        /// </summary>
        /// <param name="stockSymbol">Symbol of the stock to retrieve company profile information for.</param>
        /// <returns>A task representing the asynchronous operation with a dictionary containing company profile details.</returns>
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}
