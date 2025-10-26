namespace backend_asp_school.Models
{
    public class Parent
    {
        public int ParentID { get; set; }
        public string ParentName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public int? StudentID { get; set; }
        public Student? Student { get; set; }
    }
}
