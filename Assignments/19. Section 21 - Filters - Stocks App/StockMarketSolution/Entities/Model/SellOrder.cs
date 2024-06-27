using Entitiy.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// Represents a sell order entity in the database.
    /// </summary>
    public class SellOrder : OrderRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the sell order.
        /// </summary>
        [Key]
        public Guid SellOrderID { get; set; }
    }
}
