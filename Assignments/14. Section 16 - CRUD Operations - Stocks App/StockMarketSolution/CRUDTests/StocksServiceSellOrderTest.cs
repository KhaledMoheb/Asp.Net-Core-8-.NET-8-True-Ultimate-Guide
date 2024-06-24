using Service;
using ServiceContract;
using ServiceContract.DTO;
using Xunit;

namespace CRUDTests
{
    public class StocksServiceSellOrderTest
    {
        private readonly IStocksService _stocksService;

        //constructor
        public StocksServiceSellOrderTest()
        {
            _stocksService = new StocksService();
        }

        #region CreateSellOrder
        //When SellOrderRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task CreateSellOrder_NullSellOrder_ShouldThrowArgumentNullException()
        {
            //Arrange
            SellOrderRequest? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _stocksService.CreateSellOrder(request));
        }

        // When Quantity is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_QuantityZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 0,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(request));
        }

        // When Quantity is 100001, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_QuantityTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100001,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(request));
        }

        // When Price is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_PriceZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(request));
        }

        // When Price is 10001, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_PriceTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 10001
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(request));
        }

        // When StockSymbol is null, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_NullStockSymbol_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = null,
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(request));
        }

        // When DateAndTimeOfOrder is before 2000-01-01, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_InvalidDate_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _stocksService.CreateSellOrder(request));
        }

        // Valid request should return SellOrderResponse with a new SellOrderID
        [Fact]
        public async Task CreateSellOrder_ValidRequest_ShouldReturnProperSellOrderResponseDetails()
        {
            // Arrange
            var request = new SellOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150
            };

            // Act
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(request);
            List<SellOrderResponse> allSellOrderResponses = await _stocksService.GetSellOrders();

            // Assert
            Assert.NotNull(sellOrderResponse);
            Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
            Assert.Contains(sellOrderResponse, allSellOrderResponses);
        }
        #endregion CreateSellOrder
    }
}
