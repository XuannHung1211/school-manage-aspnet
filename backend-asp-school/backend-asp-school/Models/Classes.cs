using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend_asp_school.Models
{
    public class Classes
    {
        [Key]        
        
        public int ClassID { get; set; }
        public string ClassName { get; set; } = null!;
        public string? Room { get; set; }

        public int? TeacherID { get; set; }
        public Teacher? Teacher { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Student>? Students { get; set; }
        [JsonIgnore]
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Exam>? Exams { get; set; }
    }
}
