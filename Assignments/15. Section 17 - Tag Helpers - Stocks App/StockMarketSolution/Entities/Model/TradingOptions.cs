namespace StockMarketSolution.Models
{
    /// <summary>
    /// Represents trading options such as default stock symbol and order quantity.
    /// </summary>
    public class TradingOptions
    {
        /// <summary>
        /// Gets or sets the default stock symbol used for trading operations.
        /// </summary>
        public string? DefaultStockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the default quantity for orders placed.
        /// </summary>
        public uint DefaultOrderQuantity { get; set; }
    }
}
