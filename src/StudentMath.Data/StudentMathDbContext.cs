using Microsoft.EntityFrameworkCore;
using StudentMath.Core.Domain;
using Task = StudentMath.Core.Domain.Task;

namespace StudentMath.Data
{
    public class StudentMathDbContext : DbContext
    {
        public StudentMathDbContext(DbContextOptions<StudentMathDbContext> options)
            : base(options) { }


        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<ExamResultTask> ExamResultTask => Set<ExamResultTask>();

    }
}
