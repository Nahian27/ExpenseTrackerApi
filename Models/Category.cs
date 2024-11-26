using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models
{
    public class Category
    {
        public Guid Id { get; init; }

        [StringLength(10)]
        public required string Name { get; set; }

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}