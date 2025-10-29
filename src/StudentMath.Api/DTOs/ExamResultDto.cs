
namespace StudentMath.Api.DTOs
{
    public class ExamResultDto
    {
        public string TeacherId { get; set; } = string.Empty;
        public List<StudentResultDto> Students { get; set; } = new();
    }

    public class StudentResultDto
    {
        public string StudentId { get; set; } = string.Empty;
        public List<TaskResultDto> Tasks { get; set; } = new();
    }

    public class TaskResultDto
    {
        public string Expression { get; set; } = string.Empty;
        public double ExpectedResult { get; set; }
        public double ActualResult { get; set; }
        public bool IsCorrect { get; set; }
    }
}
