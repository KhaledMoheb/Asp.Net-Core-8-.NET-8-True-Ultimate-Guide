using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts.DTO;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;
using StockMarketSolution.Filters.ActionFilters;
using StockMarketSolution.Models.ViewModels;
using System.Text.Json;

namespace StockMarketSolution.Controllers
{
    /// <summary>
    /// Controller for handling trading operations including viewing stock information,
    /// placing buy and sell orders, and displaying order history.
    /// </summary>
    public class TradeController : Controller
    {
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubSearchStocksService _finnhubSearchStocksService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IBuyOrdersService _buyOrdersService;
        private readonly ISellOrdersService _sellOrdersService;
        private readonly ILogger<TradeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeController"/> class.
        /// </summary>
        /// <param name="finnhubService">Service for fetching company profile information.</param>
        /// <param name="tradingOptionsIOptions">Options for trading configuration.</param>
        /// <param name="configuration">Application configuration settings.</param>
        /// <param name="stocksService">Service for handling stock orders.</param>
        public TradeController(
            IFinnhubCompanyProfileService finnhubCompanyProfileService,
            IFinnhubSearchStocksService finnhubSearchStocksService,
            IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService,
            IBuyOrdersService buyOrdersService,
            ISellOrdersService sellOrdersService,
            IOptions<TradingOptions> tradingOptionsIOptions,
            IConfiguration configuration,
            ILogger<TradeController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _tradingOptions = tradingOptionsIOptions.Value;

            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubSearchStocksService = finnhubSearchStocksService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
            _buyOrdersService = buyOrdersService;
            _sellOrdersService = sellOrdersService;

        }

        /// <summary>
        /// Displays the default stock information on the Index page.
        /// </summary>
        /// <returns>The Index view with stock and company profile information.</returns>
        [Route("[action]/{stockSymbol}")]
        [Route("~/[controller]/{stockSymbol}")]
        public async Task<IActionResult> Index(string stockSymbol = "")
        {
            _logger.LogInformation($"TradeController.Index()");
            _logger.LogDebug($"TradeController Index stockSymbol: {stockSymbol}");

            if (string.IsNullOrEmpty(stockSymbol))
            {
                stockSymbol = "MSFT";
            }

            // Get current stock price quote
            var getStockPriceQuoteResponseDictionary = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);

            // Create a Stock object from the fetched data
            Stock stock = new Stock()
            {
                StockSymbol = stockSymbol,
            };

            // Get company profile information
            var getCompanyProfileResponseDictionary = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
            string companyProfileJsonString = JsonSerializer.Serialize(getCompanyProfileResponseDictionary);
            CompanyProfile companyProfile = CompanyProfile.CreateFromJson(companyProfileJsonString);

            // Create a StockTrade object combining Stock and CompanyProfile
            StockTrade stockTrade = new StockTrade(stock, companyProfile, _tradingOptions.DefaultOrderQuantity);

            // Set ViewBag for FinnhubToken to be used in the view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            // Return the Index view with the StockTrade object
            return View("Index", stockTrade);
        }

        /// <summary>
        /// Handles the creation of a new buy order.
        /// </summary>
        /// <param name="buyOrder">The buy order information.</param>
        /// <returns>The Orders view if successful; otherwise, the Index view with error messages.</returns>
        [Route("~/[controller]/BuyOrder")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrder buyOrder)
        {
            // Create a buy order using StocksService
            await _buyOrdersService.CreateBuyOrder(buyOrder);

            // After successful creation, redirect to Orders view
            return RedirectToAction(nameof(Orders));
        }

        /// <summary>
        /// Handles the creation of a new sell order.
        /// </summary>
        /// <param name="sellOrder">The sell order information.</param>
        /// <returns>The Orders view if successful; otherwise, the Index view with error messages.</returns>
        [Route("~/[controller]/SellOrder")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrder sellOrder)
        {
            // Create a sell order using StocksService
            await _sellOrdersService.CreateSellOrder(sellOrder);

            // After successful creation, redirect to Orders view
            return RedirectToAction(nameof(Orders));
        }

        /// <summary>
        /// Displays the list of buy and sell orders.
        /// </summary>
        /// <returns>The Orders view with a list of buy and sell orders.</returns>
        [Route("~/[controller]/Orders")]
        public async Task<IActionResult> Orders()
        {
            // Fetch all buy and sell orders
            Orders orders = new Orders
            {
                BuyOrders = await _buyOrdersService.GetBuyOrders(),
                SellOrders = await _sellOrdersService.GetSellOrders(),
            };

            // Return the Orders view with the fetched orders
            return View("Orders", orders);
        }

        /// <summary>
        /// Displays the list of buy and sell orders in PDF.
        /// </summary>
        /// <returns>The OrdersPDF ViewAsPdf with a list of buy and sell orders.</returns>
        [Route("~/[controller]/OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {
            // Fetch all buy and sell orders
            Orders orders = new Orders
            {
                BuyOrders = await _buyOrdersService.GetBuyOrders(),
                SellOrders = await _sellOrdersService.GetSellOrders(),
            };

            List<OrderResponse> orderResponses =
            [
                .. orders.BuyOrders,
                .. orders.SellOrders,
            ];

            orderResponses = orderResponses.OrderByDescending(orderResponse => orderResponse.DateAndTimeOfOrder).ToList();

            // Return the OrdersPDF view with the fetched orders
            return new ViewAsPdf("OrdersPDF", orderResponses);
        }
    }
}
