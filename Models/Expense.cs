using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models
{
    public class Expense
    {
        public Guid Id { get; init; }

        public required Guid CategoryId { get; set; }

        public required decimal Amount { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public required DateTime ExpenseDate { get; set; }

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

        public Category? Category { get; set; }
    }
}
