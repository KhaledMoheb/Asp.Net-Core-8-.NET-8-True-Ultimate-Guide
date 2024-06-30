using WebAPI.Core.DTO;
using System.Text.Json;
using WebAPI.Core.Entities;

namespace WebAPI.Core.Extensions
{
    public static class OrderExtensions
    {
        public static string ToJson(this Order order)
        {
            return JsonSerializer.Serialize(order);
        }

        public static string ToJson(this OrderAddRequest orderAddRequest)
        {
            return JsonSerializer.Serialize(orderAddRequest);
        }

        public static string ToJson(this OrderUpdateRequest orderUpdateRequest)
        {
            return JsonSerializer.Serialize(orderUpdateRequest);
        }

        public static string ToJson(this OrderResponse orderResponse)
        {
            return JsonSerializer.Serialize(orderResponse);
        }

        public static Order ToOrderAddRequest(this OrderAddRequest orderAddRequest)
        {
            return JsonSerializer.Deserialize<Order>(orderAddRequest.ToJson());
        }

        public static Order ToOrderResponse(this OrderResponse orderAddResponse)
        {
            return JsonSerializer.Deserialize<Order>(orderAddResponse.ToJson());
        }

        public static OrderAddRequest ToOrderAddRequest(this Order order)
        {
            return JsonSerializer.Deserialize<OrderAddRequest>(order.ToJson());
        }

        public static OrderUpdateRequest ToOrderUpdateRequest(this Order order)
        {
            return JsonSerializer.Deserialize<OrderUpdateRequest>(order.ToJson());
        }

        public static OrderResponse ToOrderResponse(this Order order)
        {
            return JsonSerializer.Deserialize<OrderResponse>(order.ToJson());
        }
    }
}
