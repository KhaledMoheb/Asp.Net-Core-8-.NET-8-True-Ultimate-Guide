using Entitiy.Model;
using System.ComponentModel.DataAnnotations;

namespace Stocks.Core.Entities.Model
{
    /// <summary>
    /// Represents a sell order entity in the database.
    /// </summary>
    public class SellOrder : Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sell order.
        /// </summary>
        [Key]
        public Guid SellOrderID { get; set; }
    }
}
