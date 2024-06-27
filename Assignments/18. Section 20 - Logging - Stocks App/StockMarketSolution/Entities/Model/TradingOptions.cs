namespace StockMarketSolution.Models
{
    /// <summary>
    /// Represents trading options such as default stock symbol and order quantity.
    /// </summary>
    public class TradingOptions
    {
        /// <summary>
        /// Gets or sets the top 25 popular stocks used for trading options
        /// </summary>
        public string? Top25PopularStocks { get; set; }

        /// <summary>
        /// Gets or sets the default quantity for orders placed.
        /// </summary>
        public uint DefaultOrderQuantity { get; set; }
    }
}
