namespace ServiceContract
{
    public interface IFinnhubStockPriceQuoteService : IFinnhubService
    {
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
