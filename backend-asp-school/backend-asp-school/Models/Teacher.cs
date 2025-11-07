using System.Security.Claims;
using System.Text.Json.Serialization;

namespace backend_asp_school.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string TeacherName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<Classes>? Classes { get; set; }
        [JsonIgnore]
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Exam>? Exams { get; set; }
    }
}
