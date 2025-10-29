using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Domain
{
    public class TeacherExam
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public Guid ExamId { get; set; }
        public Exam Exam { get; set; } = null!;
    }

}
