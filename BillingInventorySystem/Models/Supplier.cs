using System.ComponentModel.DataAnnotations;

namespace BillingInventorySystem.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string Contact { get; set; }
    }
}
