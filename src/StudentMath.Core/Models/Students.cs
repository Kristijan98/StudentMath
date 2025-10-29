public class TaskResultDtoModel
{
    public string Expression { get; set; } = string.Empty;
    public double ExpectedResult { get; set; }
    public double ActualResult { get; set; }
    public bool IsCorrect { get; set; }
}

public class ExamResultDtoModel
{
    public string StudentId { get; set; } = string.Empty;
    public int ExamId { get; set; }
    public List<TaskResultDtoModel> Tasks { get; set; } = new();
    public int TotalTasks => Tasks.Count;
    public int CorrectTasks => Tasks.Count(t => t.IsCorrect);
    public double Percentage => TotalTasks == 0 ? 0 : Math.Round((double)CorrectTasks / TotalTasks * 100, 2);
    public string Grade
    {
        get
        {
            return Percentage switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }
    }
}
