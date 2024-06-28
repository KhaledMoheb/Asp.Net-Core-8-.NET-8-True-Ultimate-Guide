using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts.FinnhubService;

namespace StockMarketSolution.ViewComponents
{
    /// <summary>
    /// View component for displaying details of a selected stock.
    /// </summary>
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly TradingOptions _tradingOptions; // Trading options configuration
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService; // Finnhub service interface
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService; // Finnhub service interface
        private readonly IConfiguration _configuration; // Application configuration

        /// <summary>
        /// Constructor to initialize dependencies for the SelectedStockViewComponent.
        /// </summary>
        /// <param name="tradingOptions">Trading options configuration injected via DI.</param>
        /// <param name="finnhubCompanyProfileService">Finnhub service interface injected via DI.</param>
        /// <param name="finnhubStockPriceQuoteService">Finnhub service interface injected via DI.</param>
        /// <param name="configuration">Application configuration injected via DI.</param>
        public SelectedStockViewComponent(
            IOptions<TradingOptions> tradingOptions,
            IFinnhubCompanyProfileService finnhubCompanyProfileService,
            IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService,
            IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value; // Initializing trading options from DI
            _finnhubCompanyProfileService = finnhubCompanyProfileService; // Initializing Finnhub service from DI
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService; // Initializing Finnhub service from DI
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
                companyProfileDict = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol); // Getting company profile
                var stockPriceDict = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol); // Getting stock price quote

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
