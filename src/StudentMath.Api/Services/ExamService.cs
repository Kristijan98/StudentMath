using Microsoft.EntityFrameworkCore;
using StudentMath.Api.DTOs;
using StudentMath.Data;
using StudentMath.Processor.Interface;

namespace StudentMath.Api.Services
{
    public class ExamService : IExamInterface
    {
        private readonly ExamProcessingService _examProcessingService;
        private readonly StudentMathDbContext _context;
        private readonly IStudentMathRepository _repository;

        public ExamService(ExamProcessingService examProcessingService, 
                           StudentMathDbContext studentMathDbContext, 
                           IStudentMathRepository repository)
        {

            _examProcessingService = examProcessingService;
            _context = studentMathDbContext;
            _repository = repository;
        }

        public async Task<IEnumerable<ExamResultSummaryModel>> GetAllExamsAsync(SearchModelStudent searchModelStudent)
        {
            var query = _context.ExamResultTask.AsQueryable();

            if (!string.IsNullOrEmpty(searchModelStudent.StudentXmlId))
                query = query.Where(ert => ert.ExamResult.Student.StudentXmlId == searchModelStudent.StudentXmlId);

            if (!string.IsNullOrEmpty(searchModelStudent.ExamXmlId))
                query = query.Where(ert => ert.ExamResult.Exam.ExamXmlId == searchModelStudent.ExamXmlId);

            if (!string.IsNullOrEmpty(searchModelStudent.TaskXmlId))
                query = query.Where(ert => ert.Task.TaskXmlId == searchModelStudent.TaskXmlId);

            var result = await query
                .OrderBy(ert => ert.ExamResult.Student.StudentXmlId)
                .ThenBy(ert => ert.ExamResult.Exam.ExamXmlId)
                .ThenBy(ert => ert.Task.TaskXmlId)
                .Select(ert => new ExamResultSummaryModel
                {
                    StudentId = ert.ExamResult.Student.StudentXmlId,
                    ExamId = ert.ExamResult.Exam.ExamXmlId,
                    TaskId = ert.Task.TaskXmlId,
                    TaskExpression = ert.Task.Expression,
                    ExpectedResult = ert.Task.ExpectedResult,
                    ActualResult = ert.ActualResult,
                    IsCorrect = ert.Task != null && ert.ActualResult == ert.Task.ExpectedResult
                })
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<List<ExamResultDtoModel>> GetStudentResultsAsync(string studentXmlId)
        {
            var results = await _context.ExamResultTask
                .Where(ert => ert.ExamResult.Student.StudentXmlId == studentXmlId)
                .Select(ert => new
                {
                    StudentId = ert.ExamResult.Student.StudentXmlId,
                    ExamId = ert.ExamResult.Exam.ExamXmlId,
                    TaskId = ert.Task.TaskXmlId,
                    ert.Task.Expression,
                    ert.Task.ExpectedResult,
                    ert.ActualResult,
                    IsCorrect = ert.Task != null && ert.ActualResult == ert.Task.ExpectedResult
                })
                .AsNoTracking()
                .ToListAsync();

            return results
                .GroupBy(r => r.ExamId)
                .Select(g => new ExamResultDtoModel
                {
                    StudentId = g.First().StudentId,
                    ExamId = int.TryParse(g.Key, out var exId) ? exId : 0,
                    Tasks = g.Select(t => new TaskResultDtoModel
                    {
                        Expression = t.Expression,
                        ExpectedResult = t.ExpectedResult,
                        ActualResult = t.ActualResult ?? 0,
                        IsCorrect = t.ActualResult == t.ExpectedResult
                    }).ToList()
                })
                .ToList();
        }

        public async Task<ExamResultDto> UploadExamTeacherAsync(IFormFile file)
        {
            string xmlContent;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                xmlContent = await reader.ReadToEndAsync();
            }

            var teacher = await _examProcessingService.ProcessExamXmlAsync(xmlContent);

            var freshTeacher = await _repository.GetTeacherByXmlIdAsync(teacher.TeacherXmlId);

            var result = new ExamResultDto
            {
                TeacherId = freshTeacher.Id.ToString(),
                Students = freshTeacher.TeacherStudents
                    .Select(ts => ts.Student)
                    .Select(s => new StudentResultDto
                    {
                        StudentId = s.StudentXmlId,
                        Tasks = s.ExamResults
                            .SelectMany(er => er.Tasks)
                            .Select(t => new TaskResultDto
                            {
                                Expression = t.Task.Expression,
                                ExpectedResult = t.Task.ExpectedResult,
                                ActualResult = t.ActualResult ?? 0,
                                IsCorrect = t.ActualResult == t.Task.ExpectedResult
                            }).ToList()
                    }).ToList()
            };

            return result;
        }


    }
}
