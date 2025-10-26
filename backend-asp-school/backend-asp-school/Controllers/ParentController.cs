using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_asp_school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParentsController(AppDbContext context)
        {
            _context = context;
        }

        // ================================
        // 🔹 GET: api/parents
        // ================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parent>>> GetParents()
        {
            var parents = await _context.Parents
                .Include(p => p.Student)
                .ToListAsync();

            return Ok(parents);
        }

        // ================================
        // 🔹 GET: api/parents/{id}
        // ================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Parent>> GetParent(int id)
        {
            var parent = await _context.Parents
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.ParentID == id);

            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        // ================================
        // 🔹 POST: api/parents
        // ================================
        [HttpPost]
        public async Task<ActionResult<Parent>> PostParent(Parent parent)
        {
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParent), new { id = parent.ParentID }, parent);
        }

        // ================================
        // 🔹 PUT: api/parents/{id}
        // ================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParent(int id, Parent updatedParent)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            parent.ParentName = updatedParent.ParentName;
            parent.Email = updatedParent.Email;
            parent.Phone = updatedParent.Phone;
            parent.Address = updatedParent.Address;
            parent.StudentID = updatedParent.StudentID;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ================================
        // 🔹 DELETE: api/parents/{id}
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParent(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
