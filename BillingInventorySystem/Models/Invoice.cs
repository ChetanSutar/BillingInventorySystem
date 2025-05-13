using System.ComponentModel.DataAnnotations;

namespace BillingInventorySystem.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<InvoiceItem> Items { get; set; }

    }
}
