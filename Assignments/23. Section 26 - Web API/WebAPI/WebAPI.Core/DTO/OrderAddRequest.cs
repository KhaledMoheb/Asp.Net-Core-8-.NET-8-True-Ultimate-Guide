using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using WebAPI.Core.Entities;

namespace WebAPI.Core.DTO
{
    public class OrderAddRequest : Order
    {
        /// <summary>
        /// Overrides the default Equals method to compare two OrderAddRequest objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not OrderAddRequest orderAddRequest)
            {
                return false;
            }

            return orderAddRequest.CustomerName == CustomerName
                && orderAddRequest.OrderNumber == OrderNumber
                && orderAddRequest.OrderDate == OrderDate
                && orderAddRequest.OrderId == OrderId
                && orderAddRequest.TotalAmount == TotalAmount;
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
