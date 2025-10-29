using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMath.Core.Domain
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TaskXmlId { get; set; } = null!;

        [Required]
        public string Expression { get; set; } = null!;

        public double ExpectedResult { get; set; }

        public ICollection<ExamTask> ExamTasks { get; set; } = new List<ExamTask>();
    }

}
