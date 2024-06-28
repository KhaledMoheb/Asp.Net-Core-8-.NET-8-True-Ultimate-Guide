using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class SearchStocksResponse
    {
        /// <summary>
        /// Number of results.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Array of search results.
        /// </summary>
        public List<SearchResult> Result { get; set; } = new List<SearchResult>();
    }

    public class SearchResult
    {
        /// <summary>
        /// Symbol description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Display symbol name.
        /// </summary>
        public string? DisplaySymbol { get; set; }

        /// <summary>
        /// Unique symbol used to identify this symbol used in /stock/candle endpoint.
        /// </summary>
        public string? Symbol { get; set; }

        /// <summary>
        /// Security type.
        /// </summary>
        public string? Type { get; set; }
    }

}
