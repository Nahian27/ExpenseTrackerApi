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
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
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
        public async Task<IActionResult> PutCategory(Guid id, [FromForm] CategoryDTO newCategory)
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
                if (!CategoryExists(id))
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
        public async Task<ActionResult<Category>> PostCategory([FromForm] CategoryDTO newCategory)
        {
            var category = new Category { Name = newCategory.Name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
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

        [HttpDelete()]
        public IActionResult BulkDelete([FromQuery] Guid[] ids)
        {
            //var idArr = ids.Split(',', StringSplitOptions.RemoveEmptyEntries);
            //foreach (var id in idArr)
            //{
            //    var cat = await _context.Categories.FindAsync(id.Trim());
            //    if(cat.Id==  Guid.TryParse(id.ToCharArray())) _context.Categories.Remove(cat);
            //}
            //await _context.SaveChangesAsync();

            return Ok(new { status = "succes", ids = ids });
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
