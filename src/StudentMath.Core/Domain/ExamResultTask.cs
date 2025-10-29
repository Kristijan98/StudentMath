using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Domain
{
    public class ExamResultTask
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ExamResultId { get; set; }
        public ExamResult ExamResult { get; set; } = null!;

        public Guid TaskId { get; set; }
        public Task Task { get; set; } = null!;

        public double? ActualResult { get; set; }

        [NotMapped]
        public bool IsCorrect {  get; set; }
    }

}
