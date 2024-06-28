using Microsoft.EntityFrameworkCore;
using Services.DbContexts;
using ServiceContracts.DTO;
using Moq;
using ServiceContracts.StocksService;
using Entity.Model;
using Services.StocksService;
using RepositoryContracts;
using AutoFixture;

namespace Tests
{
    public class StocksServiceBuyOrderTest
    {
        private readonly IFixture _fixture;

        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;

        private readonly IBuyOrdersService _buyOrdersService;

        //constructor
        public StocksServiceBuyOrderTest()
        {
            _fixture = new Fixture();
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _buyOrdersService = new StocksBuyOrdersService(_stocksRepository);
        }

        #region CreateBuyOrder
        //When BuyOrderRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ShouldThrowArgumentNullException()
        {
            //Arrange
            BuyOrder? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // When Quantity is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_QuantityZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 0,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // When Quantity is 100001, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_QuantityTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100001,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // When Price is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_PriceZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // When Price is 10001, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_PriceTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 10001
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // When StockSymbol is null, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_NullStockSymbol_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrder
            {
                StockSymbol = null,
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // When DateAndTimeOfOrder is before 2000-01-01, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_InvalidDate_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _buyOrdersService.CreateBuyOrder(request));
        }

        // Valid request should return BuyOrderResponse with a new BuyOrderID
        [Fact]
        public async Task CreateBuyOrder_ValidRequest_ShouldReturnProperBuyOrderResponseDetails()
        {
            // Arrange
            BuyOrder byuyOrderFixture = _fixture.Build<BuyOrder>().Create();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(byuyOrderFixture);
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(new List<BuyOrder> { byuyOrderFixture });

            // Act
            BuyOrderResponse buyOrderResponse = await _buyOrdersService.CreateBuyOrder(byuyOrderFixture);
            List<BuyOrderResponse> allBuyOrderResponses = await _buyOrdersService.GetBuyOrders();

            // Assert
            Assert.NotNull(buyOrderResponse);
            Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
            Assert.Contains(buyOrderResponse, allBuyOrderResponses);
        }
        #endregion CreateBuyOrder
    }
}
