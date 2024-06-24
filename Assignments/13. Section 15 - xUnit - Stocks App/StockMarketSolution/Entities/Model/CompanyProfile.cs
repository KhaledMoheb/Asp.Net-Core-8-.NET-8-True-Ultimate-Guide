using System;
using System.Text.Json;

namespace StockApp.Models
{
    [Serializable]
    public class CompanyProfile
    {
        public string country { get; set; }
        public string currency { get; set; }
        public string exchange { get; set; }
        public string finnhubIndustry { get; set; }
        public DateTime ipo { get; set; }
        public string logo { get; set; }
        public double marketCapitalization { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public double shareOutstanding { get; set; }
        public string ticker { get; set; }
        public string weburl { get; set; }

        public static CompanyProfile CreateFromJson(string jsonString) => JsonSerializer.Deserialize<CompanyProfile>(jsonString);
    }
}
