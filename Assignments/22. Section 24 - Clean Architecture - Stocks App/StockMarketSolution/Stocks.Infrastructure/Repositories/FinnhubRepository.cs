using Microsoft.Extensions.Configuration;
using Stocks.Core.RepositoryContracts;
using System.Net.Http;
using System.Text.Json;

namespace Stocks.Infrastructure.Repositories
{
    /// <summary>
    /// Service class for fetching company profile information from Finnhub API.
    /// </summary>
    public class FinnhubRepository : IFinnhubRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public string _finnhubToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FinnhubRepository"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Factory for creating HTTP clients.</param>
        /// <param name="configuration">Configuration settings.</param>
        /// <param name="finnhubToken">API token for Finnhub.</param>
        public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration, string finnhubToken)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _finnhubToken = finnhubToken;
        }

        /// <summary>
        /// Retrieves company profile information from Finnhub API.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol for which to fetch the company profile.</param>
        /// <returns>A dictionary containing the response from Finnhub API.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no response is received from Finnhub server or when there is an error in the response.</exception>
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Constructing the HTTP request to Finnhub API
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_finnhubToken}"),
                    Method = HttpMethod.Get
                };

                // Sending the HTTP request asynchronously
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                // Reading the HTTP response content as a stream
                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Reading the stream content as a string
                StreamReader streamReader = new StreamReader(stream);
                string response = await streamReader.ReadToEndAsync();

                // Deserializing the JSON response into a dictionary
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                // Handling cases where no response or error is received
                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from Finnhub server.");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException($"Finnhub API error: {responseDictionary["error"]}");
                }

                return responseDictionary;
            }
        }

        /// <summary>
        /// Retrieves stock price quote from Finnhub API.
        /// </summary>
        /// <param name="stockSymbol">Stock symbol for which to fetch the price quote.</param>
        /// <returns>A dictionary containing the response from Finnhub API.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no response is received from Finnhub server or when there is an error in the response.</exception>
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Constructing the HTTP request to Finnhub API
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_finnhubToken}"),
                    Method = HttpMethod.Get,
                };

                // Sending the HTTP request asynchronously
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                // Reading the HTTP response content as a stream
                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Reading the stream content as a string
                StreamReader streamReader = new StreamReader(stream);
                string response = await streamReader.ReadToEndAsync();

                // Deserializing the JSON response into a dictionary
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                // Handling cases where no response or error is received
                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from Finnhub server.");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException($"Finnhub API error: {responseDictionary["error"]}");
                }

                return responseDictionary;
            }
        }

        /// <summary>
        /// Retrieves a list of stock symbols from the Finnhub API.
        /// </summary>
        /// <returns>
        /// A list of dictionaries, where each dictionary represents a stock symbol and its details.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no response is received from the Finnhub server.
        /// </exception>
        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            // Using HttpClient from the factory to send HTTP requests
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Constructing the HTTP request to Finnhub API
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&&token={_finnhubToken}"),
                    Method = HttpMethod.Get,
                };

                // Sending the HTTP request asynchronously
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                // Reading the HTTP response content as a stream
                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Reading the stream content as a string
                StreamReader streamReader = new StreamReader(stream);
                string response = await streamReader.ReadToEndAsync();

                // Deserializing the JSON response into a list of dictionaries
                List<Dictionary<string, string>>? responseDictionaries = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(response);

                // Handling cases where no response or an error is received
                if (responseDictionaries == null)
                {
                    throw new InvalidOperationException("No response from Finnhub server.");
                }

                return responseDictionaries;
            }
        }

        /// <summary>
        /// Searches for a specific stock symbol using the Finnhub API.
        /// </summary>
        /// <param name="stockSymbolToSearch">The stock symbol to search for.</param>
        /// <returns>
        /// A dictionary representing the search result, containing stock details.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no response is received from the Finnhub server or if the API returns an error.
        /// </exception>
        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            // Using HttpClient from the factory to send HTTP requests
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Constructing the HTTP request to Finnhub API
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_finnhubToken}"),
                    Method = HttpMethod.Get,
                };

                // Sending the HTTP request asynchronously
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                // Reading the HTTP response content as a stream
                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();

                // Reading the stream content as a string
                StreamReader streamReader = new StreamReader(stream);
                string response = await streamReader.ReadToEndAsync();

                // Deserializing the JSON response into a dictionary
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                // Handling cases where no response or an error is received
                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from Finnhub server.");
                }

                // Handling specific error cases indicated by the API response
                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException($"Finnhub API error: {responseDictionary["error"]}");
                }

                return responseDictionary;
            }
        }
    }
}
