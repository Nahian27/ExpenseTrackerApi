using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models.DTOs
{
    public class ExpenseDTO
    {
        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }

        [Length(3, 250)]
        public string? Description { get; set; }

        public DateTime ExpenseData { get; set; }
    }
}
