using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class LessonsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LessonsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/lessons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
    {
        var lessonList = await _context.Lessons
            .Include(l => l.Subject)
            .Include(l => l.Teacher)
            .Include(l => l.Class)
            .ToListAsync();

        return Ok(lessonList);
    }

    // GET: api/lessons/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Lesson>> GetLesson(int id)
    {
        var lesson = await _context.Lessons
            .Include(l => l.Subject)
            .Include(l => l.Teacher)
            .Include(l => l.Class)
            .FirstOrDefaultAsync(l => l.LessonID == id);

        if (lesson == null)
        {
            return NotFound();
        }

        return Ok(lesson);
    }

    // POST: api/lessons
    [HttpPost]
    public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLesson", new { id = lesson.LessonID }, lesson);
    }

    // PUT: api/lessons/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLesson(int id, Lesson updatedLesson)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
        {
            return NotFound();
        }

        lesson.LessonName = updatedLesson.LessonName;
        lesson.LessonDate = updatedLesson.LessonDate;
        lesson.SubjectID = updatedLesson.SubjectID;
        lesson.TeacherID = updatedLesson.TeacherID;
        lesson.ClassID = updatedLesson.ClassID;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/lessons/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
        {
            return NotFound();
        }

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
