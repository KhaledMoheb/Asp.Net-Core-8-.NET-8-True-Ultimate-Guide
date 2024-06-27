using Entity.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace Entity.DTO
{
    /// <summary>
    /// Represents a DTO (Data Transfer Object) for the request of a sell order.
    /// </summary>
    public class SellOrderRequest
    {
        /// <summary>
        /// Gets or sets the symbol of the stock for which the sell order is placed.
        /// </summary>
        [Required]
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock for which the sell order is placed.
        /// </summary>
        [Required]
        public string StockName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sell order is placed.
        /// </summary>
        [MinDate("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stocks to be sold in the sell order.
        /// </summary>
        [Range(1, 100000, ErrorMessage = "{0} must be between {1} and {2}.")]
        public uint Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the stock in the sell order.
        /// </summary>
        [Range(1, 10000, ErrorMessage = "{0} must be between {1} and {2}.")]
        public double Price { get; set; }
    }
}
