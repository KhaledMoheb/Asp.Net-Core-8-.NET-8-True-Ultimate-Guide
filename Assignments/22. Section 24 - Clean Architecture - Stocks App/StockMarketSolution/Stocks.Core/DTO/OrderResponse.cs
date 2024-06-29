namespace Stocks.Core.DTO
{
    public class OrderResponse
    {
        /// <summary>
        /// Gets or sets the symbol of the stock for which the sell order was placed.
        /// </summary>
        public string StockSymbol { get; set; }

        /// <summary>
        /// Gets or sets the name of the stock for which the sell order was placed.
        /// </summary>
        public string StockName { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the sell order was placed.
        /// </summary>
        public DateTime DateAndTimeOfOrder { get; set; }

        /// <summary>
        /// Gets or sets the quantity of stocks sold in the sell order.
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the stock sold in the sell order.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the total trade amount (quantity * price) for the sell order.
        /// </summary>
        public double TradeAmount { get { return Quantity * Price; } }
        public OrderType TypeOfOrder { get; set; }
    }
}
