using ExpenseTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Data
{
    public class ExpenseTrackerDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Expense> Expenses { get; set; }
    }
}
