using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using ServiceContract;
using StockApp.Models;
using StockApp.Models.ViewModels;
using StocksApp.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace StockApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;

        public TradeController(
            FinnhubCompanyProfileService finnhubCompanyProfileService,
            FinnhubStockPriceQuoteService finnhubStockPriceQuoteService,
            IOptions<TradingOptions> tradingOptionsIOptions,
            IConfiguration configuration)
        {
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
            _tradingOptions = tradingOptionsIOptions.Value;
            _configuration = configuration;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultStockSymbol == null)
            {
                _tradingOptions.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object>? getStockPriceQuoteResponseDictionary = await _finnhubStockPriceQuoteService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["c"].ToString()),
                HighestPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["h"].ToString()),
                LowestPrie = Convert.ToDouble(getStockPriceQuoteResponseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(getStockPriceQuoteResponseDictionary["o"].ToString())
            };

            Dictionary<string, object>? getCompanyProfileResponseDictionary = await _finnhubCompanyProfileService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);
            string companyProfileJsonString = JsonSerializer.Serialize(getCompanyProfileResponseDictionary);
            CompanyProfile companyProfile = CompanyProfile.CreateFromJson(companyProfileJsonString);

            StockTrade stockTrade = new StockTrade(stock, companyProfile);

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}
