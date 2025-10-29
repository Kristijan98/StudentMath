using StudentMath.Core.Domain;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace StudentMath.Data
{
    public interface IStudentMathRepository
    {
        Task<Teacher?> GetTeacherByXmlIdAsync(string teacherXmlId);
        Task SaveTeacherAsync(Teacher teacher);
    }
}
