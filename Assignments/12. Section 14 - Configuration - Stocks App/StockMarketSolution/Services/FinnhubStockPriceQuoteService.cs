using Microsoft.Extensions.Configuration;
using ServiceContract;
using System.Text.Json;

namespace Service
{
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        public string _finnhubToken {  get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubStockPriceQuoteService(IHttpClientFactory httpClientFactory, IConfiguration configuration, string finnhubToken)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _finnhubToken = finnhubToken;
        }

        public async Task<Dictionary<string, object>> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_finnhubToken}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDictionary == null)
                    throw new InvalidOperationException("No response from finnhub server");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;
            }
        }
    }
}
