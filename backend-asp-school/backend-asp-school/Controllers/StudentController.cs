using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_asp_school.Controllers
{
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
        public async Task<ActionResult> GetStudents(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            // Tổng số sinh viên
            var totalRecords = await _context.Students.CountAsync();

            // Lấy danh sách sinh viên theo trang
            var students = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Results)
                    .ThenInclude(r => r.Subject)
                .Include(s => s.Parents)
                .OrderBy(s => s.StudentID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Tạo đối tượng phản hồi
            var result = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                Data = students
            };

            return Ok(result);
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
                return NotFound();

            return Ok(student);
        }

       
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, student);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

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
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
