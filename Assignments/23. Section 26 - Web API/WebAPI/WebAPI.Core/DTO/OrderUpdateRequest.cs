using WebAPI.Core.Entities;

namespace WebAPI.Core.DTO
{
    public class OrderUpdateRequest : Order
    {
        /// <summary>
        /// Overrides the default Equals method to compare two OrderUpdateRequest objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not OrderUpdateRequest orderUpdateRequest)
            {
                return false;
            }

            return orderUpdateRequest.CustomerName == CustomerName
                && orderUpdateRequest.OrderNumber == OrderNumber
                && orderUpdateRequest.OrderDate == OrderDate
                && orderUpdateRequest.OrderId == OrderId
                && orderUpdateRequest.TotalAmount == TotalAmount;
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
