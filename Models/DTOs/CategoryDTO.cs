using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models.DTOs
{
    public class CategoryDTO
    {
        [Length(3, 10)]
        public required string Name { get; set; }
    }
}
