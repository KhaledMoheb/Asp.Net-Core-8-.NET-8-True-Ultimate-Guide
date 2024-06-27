using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContract;
using StockMarketSolution.Models;

namespace StockMarketSolution.Controllers
{
    /// <summary>
    /// Controller for handling stock-related actions and views.
    /// </summary>
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly ILogger<StocksController> _logger;

        /// <summary>
        /// Constructor to initialize dependencies for the StocksController.
        /// </summary>
        /// <param name="tradingOptions">Trading options configuration injected via DI.</param>
        /// <param name="finnhubService">Finnhub service interface injected via DI.</param>
        public StocksController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, ILogger<StocksController> logger)
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _logger = logger;
        }

        /// <summary>
        /// Action method for exploring stocks based on optional stock symbol and showAll flag.
        /// </summary>
        /// <param name="stock">Optional stock symbol to filter stocks.</param>
        /// <param name="showAll">Flag to determine if all stocks should be shown or filtered.</param>
        /// <returns>Task that represents the asynchronous operation, returning IActionResult.</returns>
        [Route("/")]
        [Route("[action]/{stock?}")]
        [Route("~/[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            _logger.LogError($"StocksController.Explore()");
            _logger.LogCritical($"StocksController.Explore()");
            _logger.LogInformation($"StocksController.Explore()");
            _logger.LogDebug($"StocksController.Explore()");
            _logger.LogWarning($"StocksController.Explore()");

            // Calling service to get a list of stock dictionaries from Finnhub API
            List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

            List<Stock> stocks = new List<Stock>(); // Initializing list to store Stock objects

            if (stocksDictionary is not null)
            {
                // Filtering stocks based on popular stocks list if showAll is false and list is provided
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(",");
                    if (Top25PopularStocksList is not null)
                    {
                        stocksDictionary = stocksDictionary
                            .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
                            .ToList();
                    }
                }

                // Mapping dictionaries to Stock objects
                stocks = stocksDictionary
                    .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
                    .ToList();
            }

            ViewBag.stock = stock; // Passing stock symbol to view bag for view usage
            return View(stocks); // Returning stocks to view
        }
    }
}
