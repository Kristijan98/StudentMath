using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Domain
{
    public class ExamTask
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public Guid TaskId { get; set; }
        public Task Task { get; set; } = null!;
    }

}
