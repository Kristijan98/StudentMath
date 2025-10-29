using StudentMath.Api.IntegrationModel;
using StudentMath.Api.Services;
using StudentMath.Processor.Interface;
using System.Xml.Linq;


public class IntegrationService : IIntegrationService
{
    private readonly IMathProcessor _mathProcessor;

    public IntegrationService(IMathProcessor mathProcessor)
    {
        _mathProcessor = mathProcessor;
    }

    public async Task<IntegrationTeacherDto> ProcessExamXmlAsync(string xmlContent)
    {
        var result = new IntegrationTeacherDto();

        if (string.IsNullOrWhiteSpace(xmlContent))
        {
            result.ErrorMessage = "XML content is empty.";
            return result;
        }

        XDocument doc;
        try
        {
            doc = XDocument.Parse(xmlContent);
        }
        catch (Exception ex)
        {
            result.ErrorMessage = $"Invalid XML format: {ex.Message}";
            return result;
        }

        var teacherElem = doc.Root;
        if (teacherElem == null)
        {
            result.ErrorMessage = "Root element (Teacher) is missing.";
            return result;
        }

        result.TeacherId = teacherElem.Attribute("ID")?.Value ?? string.Empty;
        if (string.IsNullOrWhiteSpace(result.TeacherId))
        {
            result.ErrorMessage = "Teacher ID is missing.";
            return result;
        }

        var studentsElem = teacherElem.Element("Students");
        if (studentsElem == null)
        {
            result.ErrorMessage = "Students element is missing.";
            return result;
        }

        result.Students = studentsElem.Elements("Student")
            .Select(ParseStudent)
            .Where(s => s != null)
            .ToList()!;

        return result;
    }

    private IntegrationStudentDto? ParseStudent(XElement studentElem)
    {
        var studentId = studentElem.Attribute("ID")?.Value;
        if (string.IsNullOrWhiteSpace(studentId)) return null;

        var studentDto = new IntegrationStudentDto { StudentId = studentId };
        studentDto.Exams = studentElem.Elements("Exam")
            .Select(ParseExam)
            .Where(e => e != null)
            .ToList()!;

        return studentDto;
    }

    private IntegrationExamDto? ParseExam(XElement examElem)
    {
        var examId = examElem.Attribute("Id")?.Value;
        if (string.IsNullOrWhiteSpace(examId)) return null;

        var examDto = new IntegrationExamDto { ExamId = examId };
        examDto.Tasks = examElem.Elements("Task")
            .Select(ParseTask)
            .ToList();

        return examDto;
    }

    private IntegrationTaskDto ParseTask(XElement taskElem)
    {
        string taskId = taskElem.Attribute("id")?.Value ?? "Unknown";
        string text = taskElem.Value.Trim();

        if (!text.Contains("="))
        {
            return new IntegrationTaskDto
            {
                TaskId = taskId,
                Expression = text,
                IsCorrect = false,
                Error = "Missing '=' in task expression."
            };
        }

        string[] parts = text.Split('=');
        string expression = parts[0].Trim();
        double expected = double.TryParse(parts[1].Trim(), out var e) ? e : double.NaN;
        double actual = _mathProcessor.EvaluateExpression(expression);

        return new IntegrationTaskDto
        {
            TaskId = taskId,
            Expression = expression,
            ExpectedResult = expected,
            ActualResult = actual,
            IsCorrect = !double.IsNaN(expected) && actual == expected
        };
    }


}
