using Stocks.Core.Entities.Model;

namespace Stocks.Web.Models.ViewModels
{
    /// <summary>
    /// ViewModel class representing a stock trade with essential details.
    /// </summary>
    public class StockTrade
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockTrade"/> class.
        /// </summary>
        /// <param name="stock">The stock information.</param>
        /// <param name="companyProfile">The company profile information.</param>
        /// <param name="quantity">The quantity of stock traded.</param>
        public StockTrade(Stock stock, CompanyProfile companyProfile, uint quantity)
        {
            StockSymbol = stock.StockSymbol;
            StockName = companyProfile.name;
            Quantity = quantity;
        }

        public StockTrade()
        {
        }

        /// <summary>
        /// Gets or sets the stock symbol.
        /// </summary>
        public string? StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the company name associated with the stock.
        /// </summary>
        public string? StockName { get; set; }

        /// <summary>
        /// Gets or sets the price of the stock.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stock traded.
        /// </summary>
        public uint Quantity { get; set; }
    }
}
