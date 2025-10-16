using System.ComponentModel.DataAnnotations;

namespace BTL_SchoolManager.Models.Teacher
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        public String FulllName { get; set; }
        public string Subject { get; set; }
    }
}
