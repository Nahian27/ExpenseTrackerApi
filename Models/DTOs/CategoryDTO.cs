using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models.DTOs
{
    public class CategoryDTO
    {
        public Guid UserId { get; set; }

        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        // Navigation property
    }
}
