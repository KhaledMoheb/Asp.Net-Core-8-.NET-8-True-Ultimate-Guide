using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderSolution.Models
{
    // Product model with validation attributes
    public class Product
    {
        [Required] // ProductCode is required
        [DisplayName("Product Code")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be a valid number greater than zero.")]
        public int ProductCode { get; set; }

        [Required] // Price is required
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be a valid number greater than zero.")]
        [DisplayName("Product Price")]
        public double Price { get; set; }

        [Required] // Quantity is required
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be a valid number greater than zero.")]
        [DisplayName("Product Quantity")]
        public int Quantity { get; set; }
    }
}
