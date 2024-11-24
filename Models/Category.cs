using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [StringLength(10, ErrorMessage = "Category name cannot exceed 10 characters.")]
        [Length(3, 10)]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}