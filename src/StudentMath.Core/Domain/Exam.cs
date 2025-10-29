using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMath.Core.Domain
{
    public class Exam
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ExamXmlId { get; set; } = null!;

        public ICollection<TeacherExam> TeacherExams { get; set; } = new List<TeacherExam>();
        public ICollection<ExamTask> ExamTasks { get; set; } = new List<ExamTask>();
    }

}
