using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMath.Core.Domain
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string StudentXmlId { get; set; } = null!;

        public ICollection<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }

}
