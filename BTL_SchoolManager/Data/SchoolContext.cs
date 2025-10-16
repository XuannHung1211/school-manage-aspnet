using Microsoft.EntityFrameworkCore;
using BTL_SchoolManager.Models.Student;
using BTL_SchoolManager.Models.Teacher;
using BTL_SchoolManager.Models.Account;

namespace BTL_SchoolManager.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) 
            : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Account> Accounts { get; set; }    
    }
}
