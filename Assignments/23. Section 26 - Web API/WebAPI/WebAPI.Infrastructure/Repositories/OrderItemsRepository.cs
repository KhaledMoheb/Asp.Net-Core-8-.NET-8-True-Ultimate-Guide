using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Entities;
using WebAPI.Core.RepositoryContracts;
using WebAPI.Infrastructure.DBContexts;

namespace WebAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on OrderItem entities.
    /// </summary>
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly ApplicationDBContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemsRepository"/> class.
        /// </summary>
        /// <param name="applicationDBContext">The database context to be used for data operations.</param>
        public OrderItemsRepository(ApplicationDBContext applicationDBContext)
        {
            _db = applicationDBContext;
        }

        /// <summary>
        /// Creates a new OrderItem asynchronously.
        /// </summary>
        /// <param name="orderItem">The order item to create.</param>
        /// <returns>The created order item with the generated ID.</returns>
        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            orderItem.OrderItemId = Guid.NewGuid();
            await _db.OrderItems.AddAsync(orderItem);
            await _db.SaveChangesAsync();
            return orderItem;
        }

        /// <summary>
        /// Deletes an OrderItem by its ID asynchronously.
        /// </summary>
        /// <param name="orderItemId">The ID of the order item to delete.</param>
        /// <returns>True if the order item was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteOrderItemAsync(Guid orderItemId)
        {
            OrderItem? orderItem = await _db.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                _db.OrderItems.Remove(orderItem);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves all OrderItems asynchronously.
        /// </summary>
        /// <returns>A list of all order items.</returns>
        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            List<OrderItem> orderItems = await _db.OrderItems.OrderBy(orderItem => orderItem.OrderId).ToListAsync();
            foreach (OrderItem orderItem in orderItems)
            {
                orderItem.Order = await _db.Orders.FirstOrDefaultAsync(order => order.OrderId == orderItem.OrderId);
            }
            return orderItems;
        }

        /// <summary>
        /// Retrieves an OrderItem by its ID asynchronously.
        /// </summary>
        /// <param name="orderItemId">The ID of the order item to retrieve.</param>
        /// <returns>The order item with the specified ID, or null if not found.</returns>
        public async Task<OrderItem?> GetOrderItemByOrderItemIdAsync(Guid orderItemId)
        {
            OrderItem? orderItem = await _db.OrderItems.Where(orderItem => orderItem.OrderItemId == orderItemId).FirstOrDefaultAsync();
                orderItem.Order = await _db.Orders.FirstOrDefaultAsync(order => order.OrderId == orderItem.OrderId);
            return orderItem;
        }

        /// <summary>
        /// Retrieves all OrderItems for a specific Order ID asynchronously.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve items for.</param>
        /// <returns>A list of order items for the specified order ID.</returns>
        public async Task<List<OrderItem>> GetOrderItemsOfOrderIdAsync(Guid orderId)
        {
            List<OrderItem> orderItems = await _db.OrderItems.Where(orderItem => orderItem.OrderId == orderId).ToListAsync();
            foreach (OrderItem orderItem in orderItems)
            {
                orderItem.Order = await _db.Orders.FirstOrDefaultAsync(order => order.OrderId == orderItem.OrderId);
            }
            return orderItems;
        }

        /// <summary>
        /// Updates an existing OrderItem asynchronously.
        /// </summary>
        /// <param name="orderItem">The order item with updated values.</param>
        /// <returns>The updated order item.</returns>
        /// <exception cref="ArgumentException">Thrown when the order item to update does not exist.</exception>
        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            OrderItem? updatedOrderItem = await _db.OrderItems.FindAsync(orderItem.OrderItemId);
            if (updatedOrderItem == null)
            {
                throw new ArgumentException($"orderItem {orderItem.OrderItemId} does not exists");
            }

            updatedOrderItem.OrderId = orderItem.OrderId;
            updatedOrderItem.ProductName = orderItem.ProductName;
            updatedOrderItem.Quantity = orderItem.Quantity;
            updatedOrderItem.UnitPrice = orderItem.UnitPrice;
            updatedOrderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;

            await _db.SaveChangesAsync();

            return updatedOrderItem;
        }
    }
}
