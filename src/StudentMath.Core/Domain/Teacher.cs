using System.ComponentModel.DataAnnotations;

namespace StudentMath.Core.Domain
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TeacherXmlId { get; set; } = null!;

        public ICollection<TeacherStudent> TeacherStudents { get; set; } = new List<TeacherStudent>();
        public ICollection<TeacherExam> TeacherExams { get; set; } = new List<TeacherExam>();
    }

}
