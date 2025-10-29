using StudentMath.Core.Domain;
using System.Xml.Linq;
using Task = StudentMath.Core.Domain.Task;

namespace StudentMath.Core.Interface
{

    public class XmlExamParser : IXmlExamParser
    {
        public Teacher ParseXml(string xmlContent)
        {
            var xDoc = XDocument.Parse(xmlContent);
            var teacherXmlId = xDoc.Root?.Attribute("ID")?.Value
                ?? throw new Exception("Teacher ID missing.");

            var teacher = new Teacher { TeacherXmlId = teacherXmlId };

            foreach (var studentEl in xDoc.Descendants("Student"))
            {
                var studentXmlId = studentEl.Attribute("ID")?.Value;
                if (string.IsNullOrWhiteSpace(studentXmlId)) continue;

                var student = new Student { StudentXmlId = studentXmlId };
                teacher.TeacherStudents.Add(new TeacherStudent { Teacher = teacher, Student = student });

                foreach (var examEl in studentEl.Elements("Exam"))
                {
                    var examXmlId = examEl.Attribute("Id")?.Value;
                    if (string.IsNullOrWhiteSpace(examXmlId)) continue;

                    var exam = teacher.TeacherExams
                        .FirstOrDefault(te => te.Exam.ExamXmlId == examXmlId)?.Exam
                        ?? new Exam { ExamXmlId = examXmlId };

                    if (!teacher.TeacherExams.Any(te => te.Exam.Id == exam.Id))
                        teacher.TeacherExams.Add(new TeacherExam { Teacher = teacher, Exam = exam });

                    foreach (var taskEl in examEl.Elements("Task"))
                    {
                        var taskXmlId = taskEl.Attribute("id")?.Value;
                        if (string.IsNullOrWhiteSpace(taskXmlId)) continue;

                        var parts = taskEl.Value.Split('=');
                        if (parts.Length != 2 || !double.TryParse(parts[1], out var expected)) continue;

                        if (!exam.ExamTasks.Any(et => et.Task.TaskXmlId == taskXmlId))
                        {
                            var task = new Task
                            {
                                TaskXmlId = taskXmlId,
                                Expression = parts[0].Trim(),
                                ExpectedResult = expected
                            };
                            exam.ExamTasks.Add(new ExamTask { Exam = exam, Task = task });
                        }
                    }

                    student.ExamResults.Add(new ExamResult { Exam = exam, ExamId = exam.Id, Student = student, StudentId = student.Id });
                }
            }

            return teacher;
        }

    }

}


