using Microsoft.EntityFrameworkCore;
using Entity.DbContexts;
using Service;
using ServiceContract;
using Entity.DTO;
using Xunit;

namespace CRUDTests
{
    public class StocksServiceBuyOrderTest
    {
        private readonly IStocksService _stocksService;

        //constructor
        public StocksServiceBuyOrderTest()
        {
            var options = new DbContextOptionsBuilder<StockMarketDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var dbContext = new StockMarketDbContext(options);
            _stocksService = new StocksService(dbContext);
        }

        #region CreateBuyOrder
        //When BuyOrderRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ShouldThrowArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateBuyOrder(request));
        }

        // When Quantity is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_QuantityZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 0,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(request));
        }

        // When Quantity is 100001, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_QuantityTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100001,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(request));
        }

        // When Price is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_PriceZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(request));
        }

        // When Price is 10001, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_PriceTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 10001
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(request));
        }

        // When StockSymbol is null, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_NullStockSymbol_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = null,
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(request));
        }

        // When DateAndTimeOfOrder is before 2000-01-01, it should throw ArgumentException
        [Fact]
        public async Task CreateBuyOrder_InvalidDate_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateBuyOrder(request));
        }

        // Valid request should return BuyOrderResponse with a new BuyOrderID
        [Fact]
        public async Task CreateBuyOrder_ValidRequest_ShouldReturnProperBuyOrderResponseDetails()
        {
            // Arrange
            var request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150
            };

            // Act
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(request);
            List<BuyOrderResponse> allBuyOrderResponses = await _stocksService.GetBuyOrders();

            // Assert
            Assert.NotNull(buyOrderResponse);
            Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
            Assert.Contains(buyOrderResponse, allBuyOrderResponses);
        }
        #endregion CreateBuyOrder
    }
}
