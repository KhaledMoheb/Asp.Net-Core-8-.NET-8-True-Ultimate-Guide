using WebAPI.Core.Entities;

namespace WebAPI.Core.DTO
{
    public class OrderItemResponse : OrderItem
    {
        /// <summary>
        /// Overrides the default Equals method to compare two OrderItemResponse objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not OrderItemResponse orderItemResponse)
            {
                return false;
            }

            return
                orderItemResponse.OrderId == OrderId
                && orderItemResponse.OrderItemId == OrderItemId
                && orderItemResponse.Quantity == Quantity
                && orderItemResponse.ProductName == ProductName
                && orderItemResponse.UnitPrice == UnitPrice
                && orderItemResponse.TotalPrice == TotalPrice
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
