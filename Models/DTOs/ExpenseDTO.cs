using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models.DTOs
{
    public class ExpenseDTO
    {
        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExpenseData { get; set; }
    }
}
