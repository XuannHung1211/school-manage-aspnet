using System.Security.Claims;
using System.Text.Json.Serialization;

namespace backend_asp_school.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Gender { get; set; }

        public int? ClassID { get; set; }
      
        public Classes? Class { get; set; }

        public ICollection<Result>? Results { get; set; }
        public ICollection<Parent>? Parents { get; set; }
    }
}
