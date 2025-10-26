using backend_asp_school.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace backend_asp_school.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classes> Classes { get; set; } = null ;
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Parent> Parents { get; set; }
    }
}
