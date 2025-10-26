using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SubjectsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubjectsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/subjects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
    {
        var subjectList = await _context.Subjects
            .Include(s => s.Results)
            .Include(s => s.Lessons)
            .Include(s => s.Exams)
            .ToListAsync();

        return Ok(subjectList);
    }

    // GET: api/subjects/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetSubject(int id)
    {
        var subject = await _context.Subjects
            .Include(s => s.Results)
            .Include(s => s.Lessons)
            .Include(s => s.Exams)
            .FirstOrDefaultAsync(s => s.SubjectID == id);

        if (subject == null)
        {
            return NotFound();
        }

        return Ok(subject);
    }

    // POST: api/subjects
    [HttpPost]
    public async Task<ActionResult<Subject>> PostSubject(Subject subject)
    {
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubject", new { id = subject.SubjectID }, subject);
    }

    // PUT: api/subjects/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubject(int id, Subject updatedSubject)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        subject.SubjectName = updatedSubject.SubjectName;
        subject.Description = updatedSubject.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/subjects/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
