namespace Stocks.Core.DTO
{
    public class GetStocksResponse
    {
        /// <summary>
        /// Price's currency. This might be different from the reporting currency of fundamental data.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Symbol description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Display symbol name.
        /// </summary>
        public string? DisplaySymbol { get; set; }

        /// <summary>
        /// FIGI identifier.
        /// </summary>
        public string? FIGI { get; set; }

        /// <summary>
        /// ISIN. This field is only available for EU stocks and selected Asian markets.
        /// </summary>
        public string? ISIN { get; set; }

        /// <summary>
        /// Primary exchange's MIC.
        /// </summary>
        public string? MIC { get; set; }

        /// <summary>
        /// Global Share Class FIGI.
        /// </summary>
        public string? ShareClassFIGI { get; set; }

        /// <summary>
        /// Unique symbol used to identify this symbol used in /stock/candle endpoint.
        /// </summary>
        public string? Symbol { get; set; }

        /// <summary>
        /// Alternative ticker for exchanges with multiple tickers for 1 stock such as BSE.
        /// </summary>
        public string? Symbol2 { get; set; }

        /// <summary>
        /// Security type.
        /// </summary>
        public string? Type { get; set; }
    }

}
