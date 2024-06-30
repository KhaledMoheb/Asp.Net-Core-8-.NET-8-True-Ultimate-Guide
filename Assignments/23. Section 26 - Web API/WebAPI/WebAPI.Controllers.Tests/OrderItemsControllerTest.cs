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
using WebAPI.Core.ServiceContracts.OrderItems;
using WebAPI.Web.Controllers;
using Xunit;

namespace WebAPI.Controllers.Tests
{
    public class OrderItemsControllerTest
    {
        private readonly Fixture _fixture;

        private readonly Mock<ILogger<OrderItemsController>> _loggerMock;

        private readonly Mock<IOrderItemsAdderService> _orderItemsAdderServiceMock;
        private readonly Mock<IOrderItemsDeleterService> _orderItemsDeleterServiceMock;
        private readonly Mock<IOrderItemsGetterService> _orderItemsGetterServiceMock;
        private readonly Mock<IOrderItemsUpdaterService> _orderItemsUpdaterServiceMock;

        private readonly OrderItemsController _controller;

        public OrderItemsControllerTest()
        {
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<OrderItemsController>>();

            _orderItemsAdderServiceMock = new Mock<IOrderItemsAdderService>();
            _orderItemsDeleterServiceMock = new Mock<IOrderItemsDeleterService>();
            _orderItemsGetterServiceMock = new Mock<IOrderItemsGetterService>();
            _orderItemsUpdaterServiceMock = new Mock<IOrderItemsUpdaterService>();

            _controller = new OrderItemsController(
                _loggerMock.Object,
                _orderItemsGetterServiceMock.Object,
                _orderItemsAdderServiceMock.Object,
                _orderItemsUpdaterServiceMock.Object,
                _orderItemsDeleterServiceMock.Object
            );
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsListOfOrderItems()
        {
            // Arrange
            List<OrderItemResponse> expectedOrderItemResponses = _fixture.CreateMany<OrderItemResponse>(3).ToList();
            _orderItemsGetterServiceMock.Setup(svc => svc.GetAllOrderItemsAsync()).ReturnsAsync(expectedOrderItemResponses);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var orderItems = okResult.Value.Should().BeAssignableTo<List<OrderItemResponse>>().Subject;
            orderItems.Should().HaveCount(expectedOrderItemResponses.Count);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedOrder = _fixture.Build<OrderItemResponse>().With(o => o.OrderId, orderId).Create();
            _orderItemsGetterServiceMock.Setup(svc => svc.GetOrderItemByOrderItemIdAsync(orderId)).ReturnsAsync(expectedOrder);

            // Act
            var result = await _controller.Get(orderId);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var order = okResult.Value.Should().BeAssignableTo<OrderItemResponse>().Subject;
            order.OrderId.Should().Be(orderId);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderItemsGetterServiceMock.Setup(svc => svc.GetOrderItemByOrderItemIdAsync(orderId)).ReturnsAsync((OrderItemResponse)null);

            // Act
            var result = await _controller.Get(orderId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Post_WithValidOrder_ReturnsCreatedResponse()
        {
            // Arrange
            var orderToAdd = _fixture.Create<OrderItemAddRequest>();
            var expectedOrderItemResponse = orderToAdd.ToOrderItemResponse();
            _orderItemsAdderServiceMock.Setup(svc => svc.CreateOrderItemAsync(It.IsAny<OrderItemAddRequest>())).ReturnsAsync(expectedOrderItemResponse);

            // Act
            var result = await _controller.Post(orderToAdd);

            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be("Get");
            createdAtActionResult.RouteValues["id"].Should().Be(expectedOrderItemResponse.OrderItemId);
            createdAtActionResult.Value.Should().BeEquivalentTo(expectedOrderItemResponse);
        }

        [Fact]
        public async Task Post_WithInvalidOrder_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");
            var orderToAdd = new OrderItemAddRequest() { ProductName = "Test", Order = new Order() { CustomerName = "Test", OrderNumber = "Test" } };

            // Act
            var result = await _controller.Post(orderToAdd);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Put_WithValidIdAndOrder_ReturnsOkObjectResult()
        {
            // Arrange
            OrderItemUpdateRequest orderToUpdateRequest = _fixture.Build<OrderItemUpdateRequest>().Create();
            orderToUpdateRequest.ProductName = "Test";
            OrderItemResponse expectedOrderItemResponse = orderToUpdateRequest.ToOrderItemResponse();
            _orderItemsUpdaterServiceMock.Setup(svc => svc.UpdateOrderItemAsync(orderToUpdateRequest)).ReturnsAsync(expectedOrderItemResponse);

            // Act
            ActionResult<OrderItemResponse> result = await _controller.Put(orderToUpdateRequest.OrderItemId, orderToUpdateRequest);

            // Assert
            var okObjectResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeEquivalentTo(expectedOrderItemResponse);
        }

        [Fact]
        public async Task Put_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderToUpdate = _fixture.Create<OrderItemUpdateRequest>();
            _orderItemsUpdaterServiceMock.Setup(svc => svc.UpdateOrderItemAsync(It.IsAny<OrderItemUpdateRequest>())).ReturnsAsync((OrderItemResponse)null);

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
            var orderToUpdate = new OrderItemUpdateRequest() {ProductName = "Test", Order = new Order() { CustomerName = "Test", OrderNumber = "Test" } };

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
            _orderItemsDeleterServiceMock.Setup(svc => svc.DeleteOrderItemAsync(orderId)).ReturnsAsync(true);

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
            _orderItemsDeleterServiceMock.Setup(svc => svc.DeleteOrderItemAsync(orderId)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(orderId);

            // Assert
            result.Should().BeOfType<ActionResult<bool>>();
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
