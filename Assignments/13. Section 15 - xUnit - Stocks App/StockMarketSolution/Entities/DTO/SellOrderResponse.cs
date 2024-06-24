using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }

        public string StockSymbol { get; set; }

        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }
        public double TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is SellOrderResponse buyOrderResponse)
            {
                return SellOrderID == buyOrderResponse.SellOrderID
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
            return HashCode.Combine(SellOrderID, StockSymbol, StockName, DateAndTimeOfOrder, Quantity, Price, TradeAmount);
        }
    }

    public static class SellOrderExtentions
    {
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

        public static SellOrder ToSellOrder(this SellOrderResponse SellOrderRequest)
        {
            return new SellOrder
            {
                SellOrderID = SellOrderRequest.SellOrderID,
                StockSymbol = SellOrderRequest.StockSymbol,
                StockName = SellOrderRequest.StockName,
                DateAndTimeOfOrder = SellOrderRequest.DateAndTimeOfOrder,
                Quantity = SellOrderRequest.Quantity,
                Price = SellOrderRequest.Price,
            };
        }
    }
}
