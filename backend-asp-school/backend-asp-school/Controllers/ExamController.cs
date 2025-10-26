using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_asp_school.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExamsController(AppDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            var examList = await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.Class)
                .Include(e => e.Teacher)
                .ToListAsync();

            return Ok(examList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExam(int id)
        {
            var exam = await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.Class)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(e => e.ExamID == id);

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

       
        [HttpPost]
        public async Task<ActionResult<Exam>> PostExam(Exam exam)
        {
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();

            // Trả về exam vừa tạo
            return CreatedAtAction(nameof(GetExam), new { id = exam.ExamID }, exam);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExam(int id, Exam updatedExam)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            exam.ExamName = updatedExam.ExamName;
            exam.ExamDate = updatedExam.ExamDate;
            exam.SubjectID = updatedExam.SubjectID;
            exam.ClassID = updatedExam.ClassID;
            exam.TeacherID = updatedExam.TeacherID;

            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
