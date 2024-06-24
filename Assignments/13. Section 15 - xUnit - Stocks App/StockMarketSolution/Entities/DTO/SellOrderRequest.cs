using ServiceContract.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ServiceContract.DTO
{
    public class SellOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; }

        [Required]
        public string StockName { get; set; }

        [MinDateAttribute("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "{0} must be between {1} and {2}.")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "{0} must be between {1} and {2}.")]
        public double Price { get; set; }
    }
}
