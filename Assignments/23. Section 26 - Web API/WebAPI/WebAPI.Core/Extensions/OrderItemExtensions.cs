using WebAPI.Core.DTO;
using System.Text.Json;
using WebAPI.Core.Entities;

namespace WebAPI.Core.Extensions
{
    public static class OrderItemExtensions
    {
        public static string ToJson(this OrderItem orderItem)
        {
            return JsonSerializer.Serialize(orderItem);
        }

        public static string ToJson(this OrderItemAddRequest orderItemAddRequest)
        {
            return JsonSerializer.Serialize(orderItemAddRequest);
        }

        public static string ToJson(this OrderItemUpdateRequest orderItemUpdateRequest)
        {
            return JsonSerializer.Serialize(orderItemUpdateRequest);
        }

        public static string ToJson(this OrderItemResponse orderItemResponse)
        {
            return JsonSerializer.Serialize(orderItemResponse);
        }

        public static OrderItem ToOrderItemAddRequest(this OrderItemAddRequest orderItemAddRequest)
        {
            return JsonSerializer.Deserialize<OrderItem>(orderItemAddRequest.ToJson());
        }

        public static OrderItem ToOrderItemResponse(this OrderItemResponse orderItemAddResponse)
        {
            return JsonSerializer.Deserialize<OrderItem>(orderItemAddResponse.ToJson());
        }

        public static OrderItemAddRequest ToOrderItem(this OrderItem orderItem)
        {
            return JsonSerializer.Deserialize<OrderItemAddRequest>(orderItem.ToJson());
        }

        public static OrderItemUpdateRequest ToOrderUpdateRequest(this OrderItem orderItem)
        {
            return JsonSerializer.Deserialize<OrderItemUpdateRequest>(orderItem.ToJson());
        }

        public static OrderItemResponse ToOrderItemResponse(this OrderItem orderItem)
        {
            return JsonSerializer.Deserialize<OrderItemResponse>(orderItem.ToJson());
        }
    }
}
