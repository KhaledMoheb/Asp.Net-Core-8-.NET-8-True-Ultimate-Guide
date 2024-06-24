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
    }
}
