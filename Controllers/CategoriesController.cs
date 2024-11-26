using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.Models;
using ExpenseTrackerApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ExpenseTrackerDbContext _context;

        public CategoriesController(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> ListCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> SingleCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromForm] CategoryDTO newCategory)
        {
            var category = await _context.Categories.FindAsync(id);

            if (id != category!.Id)
            {
                return BadRequest();
            }
            category.Name = newCategory.Name;
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { status = "Updated Successfully" });
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromForm] CategoryDTO newCategory)
        {
            var category = new Category { Name = newCategory.Name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("SingleCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { status = "Deleted Successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> BulkDeleteCategories([FromQuery] string ids)
        {
            // Split and parse the ids
            var guidIds = ids.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(id => Guid.TryParse(id, out var guid) ? guid : (Guid?)null)
                             .Where(guid => guid.HasValue)
                             .ToArray();

            if (guidIds.Length == 0)
                return BadRequest(new { message = "Invalid GUID format in ids." });

            // Fetch and delete categories
            var categories = await _context.Categories
                                           .Where(c => guidIds.Contains(c.Id))
                                           .ToListAsync();

            if (categories.Count == 0)
                return NotFound(new { message = "Categories not found for the provided ids." });

            _context.Categories.RemoveRange(categories);
            await _context.SaveChangesAsync();

            return Ok(new { status = "success", ids = guidIds });
        }

        private async Task<bool> CategoryExists(Guid id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}
