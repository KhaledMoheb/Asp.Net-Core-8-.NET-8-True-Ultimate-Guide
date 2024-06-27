namespace Entity.DTO
{
    /// <summary>
    /// Represents a DTO (Data Transfer Object) for the response of a sell order.
    /// </summary>
    public class SellOrderResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sell order.
        /// </summary>
        public Guid SellOrderID { get; set; }

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
        public double TradeAmount { get; set; }

        /// <summary>
        /// Overrides the default Equals method to compare two SellOrderResponse objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is SellOrderResponse sellOrderResponse)
            {
                return SellOrderID == sellOrderResponse.SellOrderID
                    && StockSymbol == sellOrderResponse.StockSymbol
                    && StockName == sellOrderResponse.StockName
                    && DateAndTimeOfOrder == sellOrderResponse.DateAndTimeOfOrder
                    && Quantity == sellOrderResponse.Quantity
                    && Price == sellOrderResponse.Price
                    && TradeAmount == sellOrderResponse.TradeAmount;
            }

            return false;
        }

        /// <summary>
        /// Generates a hash code based on the properties of the SellOrderResponse object.
        /// </summary>
        /// <returns>The generated hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(SellOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price, TradeAmount);
        }
    }

    /// <summary>
    /// Contains extension methods for converting between different representations of sell orders.
    /// </summary>
    public static class SellOrderExtensions
    {
        /// <summary>
        /// Converts a SellOrderRequest object to a SellOrderResponse object.
        /// </summary>
        /// <param name="sellOrderRequest">The SellOrderRequest object to convert.</param>
        /// <returns>A new SellOrderResponse object initialized from the SellOrderRequest.</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrderRequest sellOrderRequest)
        {
            return new SellOrderResponse
            {
                StockSymbol = sellOrderRequest.StockSymbol,
                StockName = sellOrderRequest.StockName,
                DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                Quantity = sellOrderRequest.Quantity,
                Price = sellOrderRequest.Price,
            };
        }

        /// <summary>
        /// Converts a SellOrder object to a SellOrderResponse object.
        /// </summary>
        /// <param name="sellOrder">The SellOrder object to convert.</param>
        /// <returns>A new SellOrderResponse object initialized from the SellOrder.</returns>
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
            };
        }

        /// <summary>
        /// Converts a SellOrder object to a SellOrderRequest object for creating a new sell order.
        /// </summary>
        /// <param name="sellOrder">The SellOrder object to convert.</param>
        /// <returns>A new SellOrderRequest object initialized from the SellOrder.</returns>
        public static SellOrderRequest ToNewSellOrderRequest(this SellOrder sellOrder)
        {
            return new SellOrderRequest
            {
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
            };
        }

        /// <summary>
        /// Converts a SellOrderResponse object to a SellOrder object.
        /// </summary>
        /// <param name="sellOrderResponse">The SellOrderResponse object to convert.</param>
        /// <returns>A new SellOrder object initialized from the SellOrderResponse.</returns>
        public static SellOrder ToSellOrder(this SellOrderResponse sellOrderResponse)
        {
            return new SellOrder
            {
                SellOrderID = sellOrderResponse.SellOrderID,
                StockSymbol = sellOrderResponse.StockSymbol,
                StockName = sellOrderResponse.StockName,
                DateAndTimeOfOrder = sellOrderResponse.DateAndTimeOfOrder,
                Quantity = sellOrderResponse.Quantity,
                Price = sellOrderResponse.Price,
            };
        }

        /// <summary>
        /// Converts a SellOrderResponse object to an OrderResponse object.
        /// </summary>
        /// <param name="sellOrderResponse">The SellOrderResponse object to convert.</param>
        /// <returns>A new OrderResponse object initialized from the SellOrderResponse.</returns>
        public static OrderResponse ToOrderResponse(this SellOrderResponse sellOrderResponse)
        {
            return new OrderResponse
            {
                StockSymbol = sellOrderResponse.StockSymbol,
                StockName = sellOrderResponse.StockName,
                DateAndTimeOfOrder = sellOrderResponse.DateAndTimeOfOrder,
                Quantity = sellOrderResponse.Quantity,
                Price = sellOrderResponse.Price,
                TypeOfOrder = OrderType.BuyOrder,
                TradeAmount = sellOrderResponse.Quantity * sellOrderResponse.Price
            };
        }
    }
}
