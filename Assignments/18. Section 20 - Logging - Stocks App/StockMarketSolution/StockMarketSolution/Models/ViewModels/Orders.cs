using Entity.DTO; // Importing the necessary DTOs

namespace StockMarketSolution.Models.ViewModels
{
    /// <summary>
    /// ViewModel class for displaying buy and sell orders.
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Gets or sets the list of buy orders.
        /// </summary>
        public List<BuyOrderResponse> BuyOrders { get; set; }

        /// <summary>
        /// Gets or sets the list of sell orders.
        /// </summary>
        public List<SellOrderResponse> SellOrders { get; set; }
    }
}
