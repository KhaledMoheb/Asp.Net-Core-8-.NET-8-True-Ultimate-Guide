using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderSolution.CustomValidators;

namespace OrderSolution.Models
{
    // Order model with custom validation attribute
    [OrderValidation]
    public class Order
    {
        [BindNever] // Indicates that OrderNo should not be bound from request
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")] // OrderDate is required
        [DisplayName("Order Date")]
        public DateTime? OrderDate { get; set; }

        [Required] // InvoicePrice is required
        [DisplayName("Invoice Price")]
        public double InvoicePrice { get; set; }

        [Required(ErrorMessage = "At least one product is required")] // Products list is required
        public List<Product> Products { get; set; }
    }
}
