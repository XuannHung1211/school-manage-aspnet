using System.Text.Json.Serialization;

namespace backend_asp_school.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; } = null!;
        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<Result>? Results { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Exam>? Exams { get; set; }
    }
}
