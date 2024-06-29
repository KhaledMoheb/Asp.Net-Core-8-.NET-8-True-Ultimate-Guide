
using System.ComponentModel.DataAnnotations;

namespace Stocks.Core.DTO
{
    public class OrderRequest
    {
        /// <summary>
        /// Gets or sets the stock symbol for which the buy order is placed.
        /// </summary>
        [Required]
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock for which the buy order is placed.
        /// </summary>
        [Required]
        public string StockName { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the buy order.
        /// Must be greater than or equal to January 1st, 2000.
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stocks to be bought.
        /// Must be between 1 and 100,000.
        /// </summary>
        [Range(1, 100000, ErrorMessage = "{0} must be between {1} and {2}.")] // Ensures the quantity is within a specified range
        public uint Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price at which the stocks are to be bought.
        /// Must be between 1 and 10,000.
        /// </summary>
        [Range(1, 10000, ErrorMessage = "{0} must be between {1} and {2}.")] // Ensures the price is within a specified range
        public double Price { get; set; }
    }
}
