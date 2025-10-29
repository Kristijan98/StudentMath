namespace StudentMath.Api.IntegrationModel
{
    public class IntegrationTeacherDto
    {
        public string TeacherId { get; set; } = string.Empty;
        public List<IntegrationStudentDto> Students { get; set; } = new();

        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class IntegrationStudentDto
    {
        public string StudentId { get; set; } = string.Empty;
        public List<IntegrationExamDto> Exams { get; set; } = new();
    }

    public class IntegrationExamDto
    {
        public string ExamId { get; set; } = string.Empty;
        public List<IntegrationTaskDto> Tasks { get; set; } = new();
    }

    public class IntegrationTaskDto
    {
        public string TaskId { get; set; } = string.Empty;
        public string Expression { get; set; } = string.Empty;
        public double ExpectedResult { get; set; }
        public double ActualResult { get; set; }
        public bool IsCorrect { get; set; }
        public string Error { get; set; } = string.Empty;
    }


}
