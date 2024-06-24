using Microsoft.Extensions.Configuration;
using ServiceContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// Service class for fetching company profile information from Finnhub API.
    /// </summary>
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public string _finnhubToken { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FinnhubCompanyProfileService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">Factory for creating HTTP clients.</param>
        /// <param name="configuration">Configuration settings.</param>
        /// <param name="finnhubToken">API token for Finnhub.</param>
        public FinnhubCompanyProfileService(IHttpClientFactory httpClientFactory, IConfiguration configuration, string finnhubToken)
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
    }
}
