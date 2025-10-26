using System.Text.Json.Serialization;

namespace backend_asp_school.Models
{
    public class Result
    {
        public int ResultID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public double? Score { get; set; }
        public DateTime? ExamDate { get; set; }
        
        [JsonIgnore]
        public Student? Student { get; set; }

        public Subject? Subject { get; set; }
    }
}
