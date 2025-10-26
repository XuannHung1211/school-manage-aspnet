using backend_asp_school.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ResultsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ResultsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/results
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Result>>> GetResults()
    {
        var resultList = await _context.Results
            .Include(r => r.Student)
            .Include(r => r.Subject)
            .ToListAsync();

        return Ok(resultList);
    }

    // GET: api/results/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Result>> GetResult(int id)
    {
        var result = await _context.Results
            .Include(r => r.Student)
            .Include(r => r.Subject)
            .FirstOrDefaultAsync(r => r.ResultID == id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: api/results
    [HttpPost]
    public async Task<ActionResult<Result>> PostResult(Result result)
    {
        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetResult", new { id = result.ResultID }, result);
    }

    // PUT: api/results/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutResult(int id, Result updatedResult)
    {
        var result = await _context.Results.FindAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        result.StudentID = updatedResult.StudentID;
        result.SubjectID = updatedResult.SubjectID;
        result.Score = updatedResult.Score;
        result.ExamDate = updatedResult.ExamDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/results/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResult(int id)
    {
        var result = await _context.Results.FindAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        _context.Results.Remove(result);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
