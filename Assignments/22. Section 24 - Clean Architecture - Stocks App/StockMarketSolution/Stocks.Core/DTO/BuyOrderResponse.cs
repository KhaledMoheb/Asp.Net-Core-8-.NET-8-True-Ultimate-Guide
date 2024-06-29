﻿using Stocks.Core.Entities.Model;

namespace Stocks.Core.DTO
{
    /// <summary>
    /// Represents a DTO (Data Transfer Object) for the response of a buy order.
    /// </summary>
    public class BuyOrderResponse : OrderResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for the buy order.
        /// </summary>
        public Guid BuyOrderID { get; set; }

        public OrderType OrderType = OrderType.BuyOrder;

        /// <summary>
        /// Overrides the default Equals method to compare two BuyOrderResponse objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is BuyOrderResponse buyOrderResponse)
            {
                return BuyOrderID == buyOrderResponse.BuyOrderID
                    && StockSymbol == buyOrderResponse.StockSymbol
                    && StockName == buyOrderResponse.StockName
                    && DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder
                    && Quantity == buyOrderResponse.Quantity
                    && Price == buyOrderResponse.Price
                    && TradeAmount == buyOrderResponse.TradeAmount;
            }

            return false;
        }

        /// <summary>
        /// Generates a hash code based on the properties of the BuyOrderResponse object.
        /// </summary>
        /// <returns>The generated hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(BuyOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price, TradeAmount);
        }
    }

    /// <summary>
    /// Contains extension methods for converting between different representations of buy orders.
    /// </summary>
    public static class BuyOrderExtentions
    {
        /// <summary>
        /// Converts a BuyOrderRequest object to a BuyOrderResponse object.
        /// </summary>
        /// <param name="buyOrderRequest">The BuyOrderRequest object to convert.</param>
        /// <returns>A new BuyOrderResponse object initialized from the BuyOrderRequest.</returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrderRequest buyOrderRequest)
        {
            return new BuyOrderResponse
            {
                StockSymbol = buyOrderRequest.StockSymbol,
                StockName = buyOrderRequest.StockName,
                DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                Quantity = buyOrderRequest.Quantity,
                Price = buyOrderRequest.Price
            };
        }

        /// <summary>
        /// Converts a BuyOrder object to a BuyOrderResponse object.
        /// </summary>
        /// <param name="buyOrder">The BuyOrder object to convert.</param>
        /// <returns>A new BuyOrderResponse object initialized from the BuyOrder.</returns>
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price
            };
        }

        /// <summary>
        /// Converts a BuyOrder object to a BuyOrderRequest object for creating a new buy order.
        /// </summary>
        /// <param name="buyOrder">The BuyOrder object to convert.</param>
        /// <returns>A new BuyOrderRequest object initialized from the BuyOrder.</returns>
        public static BuyOrderRequest ToNewBuyOrderRequest(this BuyOrder buyOrder)
        {
            return new BuyOrderRequest
            {
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = DateTime.UtcNow,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price
            };
        }

        /// <summary>
        /// Converts a BuyOrderResponse object to a BuyOrder object.
        /// </summary>
        /// <param name="buyOrderResponse">The BuyOrderResponse object to convert.</param>
        /// <returns>A new BuyOrder object initialized from the BuyOrderResponse.</returns>
        public static BuyOrder ToBuyOrder(this BuyOrderResponse buyOrderResponse)
        {
            return new BuyOrder
            {
                BuyOrderID = buyOrderResponse.BuyOrderID,
                StockSymbol = buyOrderResponse.StockSymbol,
                StockName = buyOrderResponse.StockName,
                DateAndTimeOfOrder = buyOrderResponse.DateAndTimeOfOrder,
                Quantity = buyOrderResponse.Quantity,
                Price = buyOrderResponse.Price
            };
        }
    }
}
