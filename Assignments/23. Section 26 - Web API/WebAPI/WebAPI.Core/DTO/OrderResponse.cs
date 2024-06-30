using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Core.Entities;

namespace WebAPI.Core.DTO
{
    public class OrderResponse : Order
    {
        /// <summary>
        /// Overrides the default Equals method to compare two OrderResponse objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not OrderResponse OrderResponse)
            {
                return false;
            }

            return OrderResponse.CustomerName == CustomerName
                && OrderResponse.OrderNumber == OrderNumber
                && OrderResponse.OrderDate == OrderDate
                && OrderResponse.OrderId == OrderId
                && OrderResponse.TotalAmount == TotalAmount;
        }

        /// <summary>
        /// Generates a hash code based on the properties of the BuyOrderResponse object.
        /// </summary>
        /// <returns>The generated hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, OrderDate, OrderNumber, CustomerName, TotalAmount);
        }
    }
}
