using Entitiy.Model;
using System.ComponentModel.DataAnnotations;

namespace Stocks.Core.Entities.Model
{
    /// <summary>
    /// Represents a buy order entity in the database.
    /// </summary>
    public class BuyOrder : Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the buy order.
        /// </summary>
        [Key]
        public Guid BuyOrderID { get; set; }
    }
}
