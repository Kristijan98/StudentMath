using StudentMath.Core.Interface;
using StudentMath.Core.Domain;
using StudentMath.Data;

namespace StudentMath.Processor.Interface
{
    public class ExamProcessingService
    {
        private readonly IMathProcessor _mathProcessor;
        private readonly IXmlExamParser _xmlParser;
        private readonly IStudentMathRepository _repository;

        public ExamProcessingService(IMathProcessor mathProcessor, IXmlExamParser xmlParser, IStudentMathRepository repository)
        {
            _mathProcessor = mathProcessor;
            _xmlParser = xmlParser;
            _repository = repository;
        }

        public async Task<Teacher> ProcessExamXmlAsync(string xmlContent)
        {
            var teacher = _xmlParser.ParseXml(xmlContent);

            foreach (var ts in teacher.TeacherStudents)
            {
                var student = ts.Student;

                foreach (var examResult in student.ExamResults)
                {
                    var exam = examResult.Exam;

                    foreach (var examTask in exam.ExamTasks)
                    {
                        if (!examResult.Tasks.Any(t => t.TaskId == examTask.Task.Id))
                        {
                            var taskResult = new ExamResultTask
                            {
                                ExamResult = examResult,
                                ExamResultId = examResult.Id,
                                Task = examTask.Task,
                                TaskId = examTask.Task.Id,
                                ActualResult = _mathProcessor.EvaluateExpression(examTask.Task.Expression)
                            };
                            examResult.Tasks.Add(taskResult);
                        }
                    }
                }
            }

            await _repository.SaveTeacherAsync(teacher);
            return teacher;
        }

    }
}