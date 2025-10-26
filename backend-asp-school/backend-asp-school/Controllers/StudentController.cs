using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        var studentList = await _context.Students
            .Include(s => s.Class) 
            .Include(s => s.Results) 
                .ThenInclude(r => r.Subject) 
            .Include(s => s.Parents) // load phụ huynh (nếu có)
            .ToListAsync();

        return Ok(studentList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students
            .Include(s => s.Class)
            .Include(s => s.Results)
                .ThenInclude(r => r.Subject)
            .Include(s => s.Parents)
            .FirstOrDefaultAsync(s => s.StudentID == id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }


    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStudent", new { id = student.StudentID }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student updatedStudent)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        student.StudentName = updatedStudent.StudentName;
        student.BirthDate = updatedStudent.BirthDate;
        student.Gender = updatedStudent.Gender;
        student.ClassID = updatedStudent.ClassID;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
