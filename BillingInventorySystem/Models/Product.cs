using System.ComponentModel.DataAnnotations;

namespace BillingInventorySystem.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required, Range(0.01, 999999)]
        public decimal UnitPrice { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

    }
}
