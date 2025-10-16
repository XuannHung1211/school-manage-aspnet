using System.ComponentModel.DataAnnotations;

namespace BTL_SchoolManager.Models.Student
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string FullName { get; set; }
        public string ClassName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
