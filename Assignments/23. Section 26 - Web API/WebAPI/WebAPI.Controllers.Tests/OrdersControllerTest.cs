using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Core.DTO;
using WebAPI.Core.Entities;
using WebAPI.Core.Extensions;
using WebAPI.Core.ServiceContracts.OrderItems;
using WebAPI.Core.ServiceContracts.Orders;
using WebAPI.Web.Controllers;
using Xunit;

namespace WebAPI.Controllers.Tests
{
    public class OrdersControllerTest
    {
        private readonly Fixture _fixture;

        private readonly Mock<ILogger<OrdersController>> _loggerMock;

        private readonly Mock<IOrdersAdderService> _ordersAdderServiceMock;
        private readonly Mock<IOrdersDeleterService> _ordersDeleterServiceMock;
        private readonly Mock<IOrdersFilterService> _ordersFilterServiceMock;
        private readonly Mock<IOrdersGetterService> _ordersGetterServiceMock;
        private readonly Mock<IOrdersUpdaterService> _ordersUpdaterServiceMock;

        private readonly OrdersController _controller;

        public OrdersControllerTest()
        {
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<OrdersController>>();

            _ordersAdderServiceMock = new Mock<IOrdersAdderService>();
            _ordersDeleterServiceMock = new Mock<IOrdersDeleterService>();
            _ordersFilterServiceMock = new Mock<IOrdersFilterService>();
            _ordersGetterServiceMock = new Mock<IOrdersGetterService>();
            _ordersUpdaterServiceMock = new Mock<IOrdersUpdaterService>();

            _controller = new OrdersController(
                _loggerMock.Object,
                _ordersGetterServiceMock.Object,
                _ordersAdderServiceMock.Object,
                _ordersUpdaterServiceMock.Object,
                _ordersDeleterServiceMock.Object,
                _ordersFilterServiceMock.Object
            );
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsListOfOrders()
        {
            // Arrange
            List<OrderResponse> expectedOrderResponses = _fixture.CreateMany<OrderResponse>(3).ToList();
            _ordersGetterServiceMock.Setup(svc => svc.GetAllOrdersAsync()).ReturnsAsync(expectedOrderResponses);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var orders = okResult.Value.Should().BeAssignableTo<List<OrderResponse>>().Subject;
            orders.Should().HaveCount(expectedOrderResponses.Count);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedOrder = _fixture.Build<OrderResponse>().With(o => o.OrderId, orderId).Create();
            _ordersGetterServiceMock.Setup(svc => svc.GetOrderByOrderIdAsync(orderId)).ReturnsAsync(expectedOrder);

            // Act
            var result = await _controller.Get(orderId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var order = okResult.Value.Should().BeAssignableTo<OrderResponse>().Subject;
            order.OrderId.Should().Be(orderId);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _ordersGetterServiceMock.Setup(svc => svc.GetOrderByOrderIdAsync(orderId)).ReturnsAsync((OrderResponse)null);

            // Act
            var result = await _controller.Get(orderId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Post_WithValidOrder_ReturnsCreatedResponse()
        {
            // Arrange
            var orderToAdd = _fixture.Create<OrderAddRequest>();
            var expectedOrderResponse = orderToAdd.ToOrderResponse();
            _ordersAdderServiceMock.Setup(svc => svc.CreateOrderAsync(It.IsAny<OrderAddRequest>())).ReturnsAsync(expectedOrderResponse);

            // Act
            var result = await _controller.Post(orderToAdd);

            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be("Get");
            createdAtActionResult.RouteValues["id"].Should().Be(expectedOrderResponse.OrderId);
            createdAtActionResult.Value.Should().BeEquivalentTo(expectedOrderResponse);
        }

        [Fact]
        public async Task Post_WithInvalidOrder_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");
            var orderToAdd = new OrderAddRequest() { CustomerName = "Test", OrderNumber = "Test" };

            // Act
            var result = await _controller.Post(orderToAdd);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Put_WithValidIdAndOrder_ReturnsOkObjectResult()
        {
            // Arrange
            OrderUpdateRequest orderToUpdateRequest = _fixture.Build<OrderUpdateRequest>().Create();
            orderToUpdateRequest.CustomerName = "Test";
            OrderResponse expectedOrderResponse = orderToUpdateRequest.ToOrderResponse();
            _ordersUpdaterServiceMock.Setup(svc => svc.UpdateOrderAsync(orderToUpdateRequest)).ReturnsAsync(expectedOrderResponse);

            // Act
            ActionResult<OrderResponse> result = await _controller.Put(orderToUpdateRequest.OrderId, orderToUpdateRequest);

            // Assert
            var okObjectResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeEquivalentTo(expectedOrderResponse);
        }

        [Fact]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderToUpdate = _fixture.Create<OrderUpdateRequest>();
            _ordersUpdaterServiceMock.Setup(svc => svc.UpdateOrderAsync(It.IsAny<OrderUpdateRequest>())).ReturnsAsync((OrderResponse)null);

            // Act
            var result = await _controller.Put(orderId, orderToUpdate);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Put_WithInvalidOrder_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");
            var orderId = Guid.NewGuid();
            var orderToUpdate = new OrderUpdateRequest() { CustomerName = "Test", OrderNumber = "Test" };

            // Act
            var result = await _controller.Put(orderId, orderToUpdate);

            // Assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsTrue()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _ordersDeleterServiceMock.Setup(svc => svc.DeleteOrderAsync(orderId)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(orderId);

            // Assert
            var okObjectResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _ordersDeleterServiceMock.Setup(svc => svc.DeleteOrderAsync(orderId)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(orderId);

            // Assert
            result.Should().BeOfType<ActionResult<bool>>();
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
