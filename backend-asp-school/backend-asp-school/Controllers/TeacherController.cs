using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TeachersController : ControllerBase
{
    private readonly AppDbContext _context;

    public TeachersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/teachers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
    {
        var teacherList = await _context.Teachers
            .Include(t => t.Classes)
            .Include(t => t.Lessons)
            .Include(t => t.Exams)
            .ToListAsync();

        return Ok(teacherList);
    }

    // GET: api/teachers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Teacher>> GetTeacher(int id)
    {
        var teacher = await _context.Teachers
            .Include(t => t.Classes)
            .Include(t => t.Lessons)
            .Include(t => t.Exams)
            .FirstOrDefaultAsync(t => t.TeacherID == id);

        if (teacher == null)
        {
            return NotFound();
        }

        return Ok(teacher);
    }

    // POST: api/teachers
    [HttpPost]
    public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTeacher", new { id = teacher.TeacherID }, teacher);
    }

    // PUT: api/teachers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTeacher(int id, Teacher updatedTeacher)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        teacher.TeacherName = updatedTeacher.TeacherName;
        teacher.Email = updatedTeacher.Email;
        teacher.Phone = updatedTeacher.Phone;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/teachers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
