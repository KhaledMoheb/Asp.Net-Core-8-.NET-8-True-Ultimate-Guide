using Moq;
using Stocks.Core.ServiceContracts.StocksService;
using Stocks.Core.Entities.Model;
using Stocks.Core.DTO;
using Services.StocksService;
using AutoFixture;
using Stocks.Core.RepositoryContracts;

namespace CRUDTests
{
    public class StocksServiceSellOrderTest
    {
        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;

        private readonly ISellOrdersService _sellOrderService;
        private readonly IFixture _fixture;

        //constructor
        public StocksServiceSellOrderTest()
        {
            _fixture = new Fixture();
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _sellOrderService = new StocksSellOrdersService(_stocksRepository);
        }

        #region CreateSellOrder
        //When SellOrder is null, it should throw ArgumentNullException
        [Fact]
        public async Task CreateSellOrder_NullSellOrder_ShouldThrowArgumentNullException()
        {
            //Arrange
            SellOrder? request = null;

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // When Quantity is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_QuantityZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 0,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // When Quantity is 100001, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_QuantityTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100001,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // When Price is 0, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_PriceZero_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 0
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // When Price is 10001, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_PriceTooHigh_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 10001
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // When StockSymbol is null, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_NullStockSymbol_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrder
            {
                StockSymbol = null,
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // When DateAndTimeOfOrder is before 2000-01-01, it should throw ArgumentException
        [Fact]
        public async Task CreateSellOrder_InvalidDate_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new SellOrder
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Quantity = 100,
                Price = 150
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sellOrderService.CreateSellOrder(request));
        }

        // Valid request should return SellOrderResponse with a new SellOrderID
        [Fact]
        public async Task CreateSellOrder_ValidRequest_ShouldReturnProperSellOrderResponseDetails()
        {
            // Arrange
            SellOrder sellOrderFixture = _fixture.Build<SellOrder>().Create();

            // Mock
            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrderFixture);
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(new List<SellOrder> { sellOrderFixture });

            // Act
            SellOrderResponse sellOrderResponse = await _sellOrderService.CreateSellOrder(sellOrderFixture);
            List<SellOrderResponse> allSellOrderResponses = await _sellOrderService.GetSellOrders();

            // Assert
            Assert.NotNull(sellOrderResponse);
            Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
            Assert.Contains(sellOrderResponse, allSellOrderResponses);
        }
        #endregion CreateSellOrder
    }
}
