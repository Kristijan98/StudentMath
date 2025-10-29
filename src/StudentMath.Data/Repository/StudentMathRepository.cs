using Microsoft.EntityFrameworkCore;
using StudentMath.Core.Domain;
using Task = System.Threading.Tasks.Task;

namespace StudentMath.Data
{

    public class StudentMathRepository : IStudentMathRepository
    {
        private readonly StudentMathDbContext _context;

        public StudentMathRepository(StudentMathDbContext db)
        {
            _context = db;
        }

        public async Task<Teacher?> GetTeacherByXmlIdAsync(string teacherXmlId)
        {
            return await _context.Teachers.Include(t => t.TeacherStudents)
                                          .ThenInclude(ts => ts.Student)
                                          .Include(t => t.TeacherExams)
                                          .ThenInclude(te => te.Exam)
                                          .ThenInclude(e => e.ExamTasks)
                                          .ThenInclude(et => et.Task)
                                          .FirstOrDefaultAsync(t => t.TeacherXmlId == teacherXmlId);
        }

        public async Task SaveTeacherAsync(Teacher teacher)
        {
            if (!_context.Teachers.Local.Any(t => t.Id == teacher.Id) &&
                !await _context.Teachers.AnyAsync(t => t.Id == teacher.Id))
            {
                _context.Teachers.Add(teacher);
            }

            await _context.SaveChangesAsync();
        }
    }
}
