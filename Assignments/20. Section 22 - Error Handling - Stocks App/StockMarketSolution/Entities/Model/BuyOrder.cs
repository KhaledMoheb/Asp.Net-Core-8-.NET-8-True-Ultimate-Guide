using Entitiy.DTO;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// Represents a buy order entity in the database.
    /// </summary>
    public class BuyOrder : OrderRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the buy order.
        /// </summary>
        [Key]
        public Guid BuyOrderID { get; set; }
    }
}
