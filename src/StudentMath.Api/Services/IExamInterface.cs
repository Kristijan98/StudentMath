using StudentMath.Api.DTOs;

namespace StudentMath.Api.Services
{
    public interface IExamInterface
    {
        Task<ExamResultDto> UploadExamTeacherAsync(IFormFile file);
        Task<IEnumerable<ExamResultSummaryModel>> GetAllExamsAsync(SearchModelStudent searchModelStudent);
        Task<List<ExamResultDtoModel>> GetStudentResultsAsync(string studentXmlId);

    }
}
