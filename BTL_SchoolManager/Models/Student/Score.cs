using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_SchoolManager.Models.Student
{
    public class Score
    {
        [Key]
        public int ScoreId {  get; set; }

        [ForeignKey("StudentId")]
        public int StudentId { get; set; }

        public double Math { get; set; }
        public double English { get; set; }
        public double Science { get; set; }
        public Student Student { get; set; }    

    }
}
