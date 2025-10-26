using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ClassesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClassesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/classes
    [HttpGet]
    // GET: api/classes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetClasses()
    {
        var classList = await _context.Classes
            .Include(c => c.Teacher)
            .Select(c => new
            {
                c.ClassID,
                c.ClassName,
                c.Room,
                TeacherName = c.Teacher != null ? c.Teacher.TeacherName : null
            })
            .ToListAsync();

        return Ok(classList);
    }


    // GET: api/classes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Classes>> GetClass(int id)
    {
        var classItem = await _context.Classes
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .Include(c => c.Exams)
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.ClassID == id);

        if (classItem == null)
        {
            return NotFound();
        }

        return Ok(classItem);
    }

    

    // POST: api/classes
    [HttpPost]
    public async Task<ActionResult<Classes>> PostClass(Classes classItem)
    {
        _context.Classes.Add(classItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetClass", new { id = classItem.ClassID }, classItem);
    }

    // PUT: api/classes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClass(int id, Classes updatedClass)
    {
        var classItem = await _context.Classes.FindAsync(id);
        if (classItem == null)
        {
            return NotFound();
        }

        classItem.ClassName = updatedClass.ClassName;
        classItem.Room = updatedClass.Room;
        classItem.TeacherID = updatedClass.TeacherID;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/classes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        var classItem = await _context.Classes.FindAsync(id);
        if (classItem == null)
        {
            return NotFound();
        }

        _context.Classes.Remove(classItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
