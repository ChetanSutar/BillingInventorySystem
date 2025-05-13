using System.ComponentModel.DataAnnotations;

namespace BillingInventorySystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
