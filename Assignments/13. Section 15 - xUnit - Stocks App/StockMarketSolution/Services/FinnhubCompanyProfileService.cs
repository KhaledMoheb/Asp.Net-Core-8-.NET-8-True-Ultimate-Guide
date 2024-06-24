using Microsoft.Extensions.Configuration;
using ServiceContract;
using System.Net.Http;
using System.Text.Json;

namespace Service
{
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        public string _finnhubToken {  get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubCompanyProfileService(IHttpClientFactory httpClientFactory, IConfiguration configuration, string finnhubToken)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _finnhubToken = finnhubToken;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_finnhubToken}"),
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
