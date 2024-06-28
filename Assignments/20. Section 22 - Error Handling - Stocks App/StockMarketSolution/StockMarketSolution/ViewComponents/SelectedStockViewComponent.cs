using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContract; // Importing service contract interfaces
using StockMarketSolution.Models; // Importing model classes

namespace StockMarketSolution.ViewComponents
{
    /// <summary>
    /// View component for displaying details of a selected stock.
    /// </summary>
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly TradingOptions _tradingOptions; // Trading options configuration
        private readonly IStocksService _stocksService; // Stocks service interface
        private readonly IFinnhubService _finnhubService; // Finnhub service interface
        private readonly IConfiguration _configuration; // Application configuration

        /// <summary>
        /// Constructor to initialize dependencies for the SelectedStockViewComponent.
        /// </summary>
        /// <param name="tradingOptions">Trading options configuration injected via DI.</param>
        /// <param name="stocksService">Stocks service interface injected via DI.</param>
        /// <param name="finnhubService">Finnhub service interface injected via DI.</param>
        /// <param name="configuration">Application configuration injected via DI.</param>
        public SelectedStockViewComponent(IOptions<TradingOptions> tradingOptions, IStocksService stocksService, IFinnhubService finnhubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value; // Initializing trading options from DI
            _stocksService = stocksService; // Initializing stocks service from DI
            _finnhubService = finnhubService; // Initializing Finnhub service from DI
            _configuration = configuration; // Initializing configuration from DI
        }

        /// <summary>
        /// Invokes the view component asynchronously to fetch and display stock details.
        /// </summary>
        /// <param name="stockSymbol">Symbol of the selected stock.</param>
        /// <returns>Task that represents the asynchronous operation, returning an IViewComponentResult.</returns>
        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileDict = null; // Dictionary to store company profile details

            // Fetching company profile details and stock price asynchronously if stockSymbol is not null
            if (stockSymbol != null)
            {
                companyProfileDict = await _finnhubService.GetCompanyProfile(stockSymbol); // Getting company profile
                var stockPriceDict = await _finnhubService.GetStockPriceQuote(stockSymbol); // Getting stock price quote

                // Adding stock price to company profile if both profile and price are fetched successfully
                if (stockPriceDict != null && companyProfileDict != null)
                {
                    companyProfileDict.Add("price", stockPriceDict["c"]); // Adding price to company profile dictionary
                }
            }

            // Returning a view with company profile data if logo exists, otherwise returning an empty content
            if (companyProfileDict != null && companyProfileDict.ContainsKey("logo"))
                return View(companyProfileDict); // Returning view with company profile data
            else
                return Content(""); // Returning empty content if company profile or logo doesn't exist
        }
    }
}
