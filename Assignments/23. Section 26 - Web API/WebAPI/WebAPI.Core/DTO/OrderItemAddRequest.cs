using WebAPI.Core.Entities;

namespace WebAPI.Core.DTO
{
    public class OrderItemAddRequest : OrderItem
    {
        /// <summary>
        /// Overrides the default Equals method to compare two OrderItemAddRequest objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not OrderItemAddRequest orderItemAddRequest)
            {
                return false;
            }

            return
                orderItemAddRequest.OrderId == OrderId
                && orderItemAddRequest.OrderItemId == OrderItemId
                && orderItemAddRequest.Quantity == Quantity
                && orderItemAddRequest.ProductName == ProductName
                && orderItemAddRequest.UnitPrice == UnitPrice
                && orderItemAddRequest.TotalPrice == TotalPrice
                ;
        }

        /// <summary>
        /// Generates a hash code based on the properties of the BuyOrderResponse object.
        /// </summary>
        /// <returns>The generated hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, OrderItemId, Quantity, ProductName, UnitPrice, TotalPrice);
        }
    }
}
