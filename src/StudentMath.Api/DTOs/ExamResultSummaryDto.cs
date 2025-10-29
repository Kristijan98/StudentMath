namespace StudentMath.Api.DTOs
{
    public class ExamResultSummaryModel
    {
        public string StudentId { get; set; } = string.Empty;
        public string ExamId { get; set; } = string.Empty;
        public string TaskId { get; set; } = string.Empty;
        public string TaskExpression { get; set; } = string.Empty;
        public double ExpectedResult { get; set; }
        public double? ActualResult { get; set; }
        public bool IsCorrect { get; set; }
    }
}
