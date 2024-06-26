using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// Represents a buy order entity in the database.
    /// </summary>
    public class BuyOrder
    {
        /// <summary>
        /// Gets or sets the unique identifier for the buy order.
        /// </summary>
        [Key]
        public Guid BuyOrderID { get; set; }

        /// <summary>
        /// Gets or sets the symbol of the stock for which the buy order is placed.
        /// </summary>
        [Required]
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock for which the buy order is placed.
        /// </summary>
        [Required]
        public string StockName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the buy order was placed.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stocks purchased in the buy order.
        /// </summary>
        [Range(1, 100000)]
        public uint Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the stock purchased in the buy order.
        /// </summary>
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
