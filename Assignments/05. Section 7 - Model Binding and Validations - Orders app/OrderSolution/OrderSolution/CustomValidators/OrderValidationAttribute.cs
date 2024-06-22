using System.ComponentModel.DataAnnotations;
using OrderSolution.Models;

namespace OrderSolution.CustomValidators
{
    // Custom validation attribute for order validation
    public class OrderValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Retrieve the order instance
            var order = (Order)validationContext.ObjectInstance;

            // Calculate the total cost of products in the order
            double totalCost = order.Products.Sum(p => p.Price * p.Quantity);

            // Compare invoice price with calculated total cost
            if (order.InvoicePrice != totalCost)
            {
                // Return validation error if invoice price doesn't match total cost
                return new ValidationResult($"InvoicePrice ({order.InvoicePrice}) doesn't match the total cost of the specified products ({totalCost}).");
            }

            // Validation successful if no errors found
            return ValidationResult.Success;
        }
    }
}
