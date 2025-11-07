using System.Security.Claims;
using System.Text.Json.Serialization;

namespace backend_asp_school.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public string? LessonName { get; set; }
        public DateTime? LessonDate { get; set; }

        public int? SubjectID { get; set; }
      
        public Subject? Subject { get; set; }

        public int? TeacherID { get; set; }
       
        public Teacher? Teacher { get; set; }

        public int? ClassID { get; set; }
       
        public Classes? Class { get; set; }
    }
}
