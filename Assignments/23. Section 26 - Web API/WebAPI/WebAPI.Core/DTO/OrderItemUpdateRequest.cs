using WebAPI.Core.Entities;

namespace WebAPI.Core.DTO
{
    public class OrderItemUpdateRequest : OrderItem
    {
        /// <summary>
        /// Overrides the default Equals method to compare two OrderItemUpdateRequest objects.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj is not OrderItemUpdateRequest orderItemUpdateRequest)
            {
                return false;
            }

            return 
                orderItemUpdateRequest.OrderId == OrderId
                && orderItemUpdateRequest.OrderItemId == OrderItemId
                && orderItemUpdateRequest.Quantity == Quantity
                && orderItemUpdateRequest.ProductName == ProductName
                && orderItemUpdateRequest.UnitPrice == UnitPrice
                && orderItemUpdateRequest.TotalPrice == TotalPrice
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
