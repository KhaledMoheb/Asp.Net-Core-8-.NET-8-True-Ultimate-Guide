using System;
using System.Text.Json;

namespace StockMarketSolution.Models
{
    /// <summary>
    /// Represents company profile information retrieved from Finnhub API.
    /// </summary>
    [Serializable]
    public class CompanyProfile
    {
        /// <summary>
        /// Gets or sets the country of the company.
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Gets or sets the currency used by the company.
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// Gets or sets the exchange on which the company is listed.
        /// </summary>
        public string exchange { get; set; }

        /// <summary>
        /// Gets or sets the industry classification from Finnhub.
        /// </summary>
        public string finnhubIndustry { get; set; }

        /// <summary>
        /// Gets or sets the date of the company's IPO.
        /// </summary>
        public DateTime ipo { get; set; }

        /// <summary>
        /// Gets or sets the URL of the company's logo.
        /// </summary>
        public string logo { get; set; }

        /// <summary>
        /// Gets or sets the market capitalization of the company.
        /// </summary>
        public double marketCapitalization { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the company.
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// Gets or sets the number of outstanding shares of the company.
        /// </summary>
        public double shareOutstanding { get; set; }

        /// <summary>
        /// Gets or sets the stock ticker symbol of the company.
        /// </summary>
        public string ticker { get; set; }

        /// <summary>
        /// Gets or sets the company's website URL.
        /// </summary>
        public string weburl { get; set; }

        /// <summary>
        /// Creates a <see cref="CompanyProfile"/> object from a JSON string.
        /// </summary>
        /// <param name="jsonString">JSON string representing company profile data.</param>
        /// <returns>A new instance of <see cref="CompanyProfile"/>.</returns>
        public static CompanyProfile CreateFromJson(string jsonString) => JsonSerializer.Deserialize<CompanyProfile>(jsonString);
    }
}
