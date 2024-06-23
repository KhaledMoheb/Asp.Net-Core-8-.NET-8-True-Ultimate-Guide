namespace StockMarketSolution.Models.ViewModels
{
    public class StockTrade
    {
        public StockTrade(Stock stock, CompanyProfile companyProfile)
        {
            StockSymbol = stock.StockSymbol;
            StockName = companyProfile.name;
            Price = stock.HighestPrice;
        }

        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}
