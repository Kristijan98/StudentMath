using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMath.Core.Domain
{
    public class TeacherStudent
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }

}
