using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.ServiceContracts.OrderItems;

namespace WebAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IOrderItemsGetterService _orderItemsGetterService;
        private readonly IOrderItemsAdderService _orderItemsAdderService;
        private readonly IOrderItemsUpdaterService _orderItemsUpdaterService;
        private readonly IOrderItemsDeleterService _orderItemsDeleterService;

        public OrderItemsController(
            ILogger<OrderItemsController> logger,
            IOrderItemsGetterService orderItemsGetterService,
            IOrderItemsAdderService orderItemsAdderService,
            IOrderItemsUpdaterService orderItemsUpdaterService,
            IOrderItemsDeleterService orderItemsDeleterService)
        {
            _logger = logger;
            _orderItemsGetterService = orderItemsGetterService;
            _orderItemsAdderService = orderItemsAdderService;
            _orderItemsUpdaterService = orderItemsUpdaterService;
            _orderItemsDeleterService = orderItemsDeleterService;
        }

        /// <summary>
        /// Retrieves all order items.
        /// </summary>
        /// <returns>An action result containing a list of order items.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderItemResponse>), 200)]
        public async Task<ActionResult<List<OrderItemResponse>>> Get()
        {
            var orderItems = await _orderItemsGetterService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        /// <summary>
        /// Retrieves an order item by its ID.
        /// </summary>
        /// <param name="id">The ID of the order item to retrieve.</param>
        /// <returns>An action result containing the retrieved order item.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderItemResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderItemResponse>> Get(Guid id)
        {
            var orderItem = await _orderItemsGetterService.GetOrderItemByOrderItemIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        /// <summary>
        /// Retrieves all order items of a specific order ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to retrieve order items for.</param>
        /// <returns>An action result containing a list of order items.</returns>
        [HttpGet("order/{orderId}")]
        [ProducesResponseType(typeof(List<OrderItemResponse>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<OrderItemResponse>>> GetOrderItemsOfOrderId(Guid orderId)
        {
            var orderItems = await _orderItemsGetterService.GetOrderItemsOfOrderIdAsync(orderId);
            if (orderItems == null || orderItems.Count == 0)
            {
                return NotFound();
            }
            return Ok(orderItems);
        }

        /// <summary>
        /// Creates a new order item.
        /// </summary>
        /// <param name="orderItem">The order item data to create.</param>
        [HttpPost]
        [ProducesResponseType(typeof(OrderItemResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderItemResponse>> Post([FromBody] OrderItemAddRequest orderItem)
        {
            try
            {
                var createdOrderItem = await _orderItemsAdderService.CreateOrderItemAsync(orderItem);
                return CreatedAtAction(nameof(Get), new { id = createdOrderItem.OrderItemId }, createdOrderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order item");
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates an existing order item.
        /// </summary>
        /// <param name="id">The ID of the order item to update.</param>
        /// <param name="orderItem">The updated order item data.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderItemResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderItemResponse>> Put(Guid id, [FromBody] OrderItemUpdateRequest orderItem)
        {
            if (id != orderItem.OrderItemId)
            {
                return BadRequest("Order item ID mismatch");
            }

            try
            {
                var updatedOrderItem = await _orderItemsUpdaterService.UpdateOrderItemAsync(orderItem);
                return Ok(updatedOrderItem);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error updating order item");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order item");
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes an order item by its ID.
        /// </summary>
        /// <param name="id">The ID of the order item to delete.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                var deletionResult = await _orderItemsDeleterService.DeleteOrderItemAsync(id);
                if (!deletionResult)
                {
                    return NotFound();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order item");
                return BadRequest();
            }
        }
    }
}
