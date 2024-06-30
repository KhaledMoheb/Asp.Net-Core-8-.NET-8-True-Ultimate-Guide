using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Core.Entities
{
    /// <summary>
    /// Represents the individual items within each order
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// A unique identifier for the order item. (Primary Key)
        /// </summary>
        [Key]
        [Display(Name = "Order Item Id")]
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// The identifier of the order to which the item belongs.
        /// (Foreign Key to Order table)
        /// Validation Rule: Required field.
        /// </summary>
        [ForeignKey(nameof(Order))]
        [Required]
        [Display(Name = "Order Id")]
        public Guid OrderId { get; set; }

        /// <summary>
        /// The name of the product in the order item.
        /// Validation Rule: Required field, maximum length of 50 characters.
        /// </summary>
        [Required]
        [Display(Name = "Product Name")]
        [StringLength(50, ErrorMessage = "{0} maximum is {1} characters.")]
        public required string ProductName { get; set; }

        /// <summary>
        /// The quantity of the product in the order item.
        /// Validation Rule: Must be a positive number.
        /// </summary>
        [Required]
        [Range(1, uint.MaxValue, ErrorMessage = "{0} Must be a positive number.")]
        public uint Quantity { get; set; }

        /// <summary>
        /// The unit price of the product in the order item.
        /// Validation Rule: Must be a positive number.
        /// </summary>
        [Required]
        [Display(Name = "Unit Price")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} Must be a positive number.")]
        [Column(TypeName = "decimal")]
        public double UnitPrice { get; set; }

        /// <summary>
        /// The total price of the order item (Quantity * UnitPrice).
        /// Auto-Generation Rule: The TotalPrice should be calculated automatically based on the Quantity and UnitPrice.
        /// </summary>
        [Column(TypeName = "decimal")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double TotalPrice { get; set; }

        public virtual required Order Order { get; set; }
    }
}
