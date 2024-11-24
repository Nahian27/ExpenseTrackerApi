using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController(ExpenseTrackerDbContext context) : ControllerBase
    {

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await context.Expenses.Include("Category").ToArrayAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(Guid id)
        {
            var expense = await context.Expenses.Include("Category").FirstOrDefaultAsync(x => x.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(Guid id, [FromForm][FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            context.Entry(expense).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense([FromForm][FromBody] Expense expense)
        {
            context.Expenses.Add(expense);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var expense = await context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(Guid id)
        {
            return context.Expenses.Any(e => e.Id == id);
        }
    }
}
