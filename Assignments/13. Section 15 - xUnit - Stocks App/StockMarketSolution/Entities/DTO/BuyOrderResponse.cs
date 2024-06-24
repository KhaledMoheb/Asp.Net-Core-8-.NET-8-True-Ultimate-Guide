using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }

        public string StockSymbol { get; set; }

        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder;

        public uint Quantity { get; set; }

        public double Price { get; set; }
        public double TradeAmount { get; set; }

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

        public override int GetHashCode()
        {
            return HashCode.Combine(BuyOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price, TradeAmount);
        }
    }

    public static class BuyOrderExtentions
    {
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

        public static BuyOrder ToBuyOrder(this BuyOrderResponse buyOrderRequest)
        {
            return new BuyOrder
            {
                BuyOrderID = buyOrderRequest.BuyOrderID,
                StockSymbol = buyOrderRequest.StockSymbol,
                StockName = buyOrderRequest.StockName,
                DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                Quantity = buyOrderRequest.Quantity,
                Price = buyOrderRequest.Price
            };
        }
    }
}
