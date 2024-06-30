using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Core.Entities
{
    /// <summary>
    /// Represents customer orders
    /// </summary>
    public class Order
    {
        /// <summary>
        /// A unique identifier for the order. (Primary Key)
        /// </summary>
        [Key]
        [Display(Name = "Order Id")]
        public Guid OrderId { get; set; }

        /// <summary>
        /// A system-generated order number for easy identification.
        /// Auto-Generation Rule: The OrderNumber should be auto-generated using a sequential number or any desired pattern.
        /// e.g: Order_2024_1, Order_2024_2, Order_2024_3 etc. The year should be automatically generated as current year.
        /// </summary>
        [Display(Name = "Order Number")]
        [RegularExpression(@"^Order_\d{4}_\d+$", ErrorMessage = "{0} should begin with 'Order_' followed by a year then an underscore (_) and a sequential number.")]
        public required string OrderNumber { get; set; }


        /// <summary>
        /// The name of the customer who placed the order.
        /// Validation Rule: Required field, maximum length of 50 characters.
        /// </summary>
        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(50, ErrorMessage = "{0} maximum is {1} characters.")]
        public required string CustomerName { get; set; }

        /// <summary>
        /// The date when the order was placed.
        /// Validation Rule: Required field.
        /// </summary>
        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The total amount of the order.
        /// Validation Rule: Must be a positive number.
        /// </summary>
        [Required]
        [Display(Name = "Total Amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "{0} Must be a positive number.")]
        [Column(TypeName = "decimal")]
        public double TotalAmount { get; set; }
    }
}
