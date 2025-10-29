using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Domain
{
    public class ExamResult
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public ICollection<ExamResultTask> Tasks { get; set; } = new List<ExamResultTask>();
    }

}
