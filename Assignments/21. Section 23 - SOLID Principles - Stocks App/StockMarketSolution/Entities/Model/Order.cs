using Services.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace Entitiy.Model
{
    public class Order
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
        /// Gets or sets the date and time when the sell order was placed.
        /// </summary>
        [MinDate("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stocks to be sold in the sell order.
        /// </summary>
        [Range(1, 100000)]
        public uint Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the stock in the sell order.
        /// </summary>
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
