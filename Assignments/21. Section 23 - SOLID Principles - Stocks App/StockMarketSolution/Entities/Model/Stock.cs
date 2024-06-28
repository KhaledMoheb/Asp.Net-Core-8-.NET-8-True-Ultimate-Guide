namespace Entity.Model
{
    /// <summary>
    /// Represents a stock with symbol and name.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// The symbol of the stock.
        /// </summary>
        public string? StockSymbol { get; set; }

        /// <summary>
        /// The name of the stock.
        /// </summary>
        public string? StockName { get; set; }

        /// <summary>
        /// Checks whether this stock is equal to another object.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Stock other) return false;

            return StockSymbol == other.StockSymbol && StockName == other.StockName;
        }

        /// <summary>
        /// Generates a hash code for this stock.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(StockSymbol, StockName);
        }
    }
}
