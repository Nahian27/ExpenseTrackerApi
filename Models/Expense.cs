using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models
{
    public class Expense
    {
        public Guid Id { get; set; }

        public required Guid CategoryId { get; set; }

        public required decimal Amount { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public required DateTime ExpenseDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Category? Category { get; set; }
    }
}
