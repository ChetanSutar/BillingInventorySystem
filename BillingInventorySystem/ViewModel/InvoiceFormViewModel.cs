using BillingInventorySystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BillingInventorySystem.ViewModel
{
    public class InvoiceFormViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public List<InvoiceItemViewModel> Items { get; set; } = new List<InvoiceItemViewModel>();

        // For dropdowns
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
    }

    public class InvoiceItemViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
