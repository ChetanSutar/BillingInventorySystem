using System.ComponentModel.DataAnnotations;

namespace BillingInventorySystem.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required, Range(1, 1000)]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public decimal Total => Quantity * Product.UnitPrice;

    }
}
