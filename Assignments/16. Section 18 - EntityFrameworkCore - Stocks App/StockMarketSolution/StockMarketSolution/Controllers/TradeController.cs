using Entity;
using Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using Service;
using ServiceContract;
using StockMarketSolution.Models;
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
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IStocksService _stocksService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeController"/> class.
        /// </summary>
        /// <param name="finnhubService">Service for fetching company profile information.</param>
        /// <param name="tradingOptionsIOptions">Options for trading configuration.</param>
        /// <param name="configuration">Application configuration settings.</param>
        /// <param name="stocksService">Service for handling stock orders.</param>
        public TradeController(
            FinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptionsIOptions,
            IConfiguration configuration,
            IStocksService stocksService)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptionsIOptions.Value;
            _configuration = configuration;
            _stocksService = stocksService;
        }

        /// <summary>
        /// Displays the default stock information on the Index page.
        /// </summary>
        /// <returns>The Index view with stock and company profile information.</returns>
        [Route("Trade/Index")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            // Get current stock price quote
            var getStockPriceQuoteResponseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            // Create a Stock object from the fetched data
            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["h"].ToString()),
                LowestPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["o"].ToString()),
            };

            // Get company profile information
            var getCompanyProfileResponseDictionary = await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
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
        [Route("Trade/BuyOrder")]
        public async Task<IActionResult> BuyOrder(BuyOrder buyOrder)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                // If model state is invalid, set ViewBag with error messages and return to Index view
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return await Index();
            }

            // Convert BuyOrder to BuyOrderRequest
            BuyOrderRequest buyOrderRequest = buyOrder.ToNewBuyOrderRequest();

            // Create a buy order using StocksService
            await _stocksService.CreateBuyOrder(buyOrderRequest);

            // After successful creation, redirect to Orders view
            return await Orders();
        }

        /// <summary>
        /// Handles the creation of a new sell order.
        /// </summary>
        /// <param name="sellOrder">The sell order information.</param>
        /// <returns>The Orders view if successful; otherwise, the Index view with error messages.</returns>
        [Route("Trade/SellOrder")]
        public async Task<IActionResult> SellOrder(SellOrder sellOrder)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                // If model state is invalid, set ViewBag with error messages and return to Index view
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return await Index();
            }

            // Convert SellOrder to SellOrderRequest
            SellOrderRequest sellOrderRequest = sellOrder.ToNewSellOrderRequest();

            // Create a sell order using StocksService
            await _stocksService.CreateSellOrder(sellOrderRequest);

            // After successful creation, redirect to Orders view
            return await Orders();
        }

        /// <summary>
        /// Displays the list of buy and sell orders.
        /// </summary>
        /// <returns>The Orders view with a list of buy and sell orders.</returns>
        [Route("Trade/Orders")]
        public async Task<IActionResult> Orders()
        {
            // Fetch all buy and sell orders
            Orders orders = new Orders
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders(),
            };

            // Return the Orders view with the fetched orders
            return View("Orders", orders);
        }

        /// <summary>
        /// Displays the list of buy and sell orders in PDF.
        /// </summary>
        /// <returns>The OrdersPDF ViewAsPdf with a list of buy and sell orders.</returns>
        [Route("Trade/OrdersPDF")]
        public async Task<IActionResult> OrdersPDF()
        {
            // Fetch all buy and sell orders
            Orders orders = new Orders
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders(),
            };

            List<OrderResponse> orderResponses =
            [
                .. orders.BuyOrders.Select(buyOrder => buyOrder.ToOrderResponse()),
                .. orders.SellOrders.Select(sellOrder => sellOrder.ToOrderResponse()),
            ];

            orderResponses = orderResponses.OrderByDescending(orderResponse => orderResponse.DateAndTimeOfOrder).ToList();

            // Return the OrdersPDF view with the fetched orders
            return new ViewAsPdf("OrdersPDF", orderResponses);
        }
    }
}
