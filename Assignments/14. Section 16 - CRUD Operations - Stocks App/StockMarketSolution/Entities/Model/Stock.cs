namespace StockMarketSolution.Models
{
    /// <summary>
    /// Represents stock information including symbol, prices, and statistics.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Gets or sets the symbol of the stock.
        /// </summary>
        public string? StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the current price of the stock.
        /// </summary>
        public double CurrentPrice { get; set; }

        /// <summary>
        /// Gets or sets the lowest price of the stock within a period.
        /// </summary>
        public double LowestPrice { get; set; }

        /// <summary>
        /// Gets or sets the highest price of the stock within a period.
        /// </summary>
        public double HighestPrice { get; set; }

        /// <summary>
        /// Gets or sets the opening price of the stock.
        /// </summary>
        public double OpenPrice { get; set; }
    }
}
