using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderID { get; set; }

        [Required]
        public string StockSymbol { get; set; }

        [Required]
        public string StockName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAndTimeOfOrder;

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price {  get; set; }
    }
}
