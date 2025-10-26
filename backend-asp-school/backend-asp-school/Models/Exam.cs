using System.Security.Claims;

namespace backend_asp_school.Models
{
    public class Exam
    {
        public int ExamID { get; set; }
        public string? ExamName { get; set; }
        public DateTime? ExamDate { get; set; }

        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }

        public int? ClassID { get; set; }
        public Classes? Class { get; set; }

        public int? TeacherID { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
