using Microsoft.AspNetCore.Mvc;
using WebAPI.Core.DTO;
using WebAPI.Core.ServiceContracts.Orders;
using WebAPI.Core.Services.Orders;

namespace WebAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersGetterService _ordersGetterService;
        private readonly IOrdersAdderService _ordersAdderService;
        private readonly IOrdersUpdaterService _ordersUpdaterService;
        private readonly IOrdersDeleterService _ordersDeleterService;
        private readonly IOrdersFilterService _ordersFilterService;

        public OrdersController(
            ILogger<OrdersController> logger,
            IOrdersGetterService ordersGetterService,
            IOrdersAdderService ordersAdderService,
            IOrdersUpdaterService ordersUpdaterService,
            IOrdersDeleterService ordersDeleterService,
            IOrdersFilterService ordersFilterService)
        {
            _logger = logger;
            _ordersGetterService = ordersGetterService;
            _ordersAdderService = ordersAdderService;
            _ordersUpdaterService = ordersUpdaterService;
            _ordersDeleterService = ordersDeleterService;
            _ordersFilterService = ordersFilterService;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>An action result containing a list of orders.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderResponse>), 200)]
        public async Task<ActionResult<List<OrderResponse>>> Get()
        {
            var orders = await _ordersGetterService.GetAllOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>An action result containing the retrieved order.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderResponse>> Get(Guid id)
        {
            var order = await _ordersGetterService.GetOrderByOrderIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="order">The order data to create.</param>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderResponse>> Post([FromBody] OrderAddRequest order)
        {
            try
            {
                var createdOrder = await _ordersAdderService.CreateOrderAsync(order);
                return CreatedAtAction(nameof(Get), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The updated order data.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderResponse>> Put(Guid id, [FromBody] OrderUpdateRequest order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Order ID mismatch");
            }

            try
            {
                OrderResponse orderRespose = await _ordersUpdaterService.UpdateOrderAsync(order);
                return Ok(orderRespose);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order");
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                var deletionResult = await _ordersDeleterService.DeleteOrderAsync(id);
                if (!deletionResult)
                {
                    return NotFound();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order");
                return BadRequest();
            }
        }
    }
}
