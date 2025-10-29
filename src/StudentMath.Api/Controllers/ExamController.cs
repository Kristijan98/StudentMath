using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMath.Api.DTOs;
using StudentMath.Api.Services;
using static StudentMath.Ui.Pages.TeacherDashboard;

namespace StudentMath.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {

        private readonly IExamInterface _examService;

        public ExamController(IExamInterface examService)
        {
            _examService = examService;
        }

        [HttpPost("upload")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UploadExam([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var result = _examService.UploadExamTeacherAsync(file);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] string? studentXmlId = null,
                                                [FromQuery] string? examXmlId = null,
                                                [FromQuery] string? taskXmlId = null)
        {

            var searchModel = new SearchModelStudent
            {
                StudentXmlId = studentXmlId,
                ExamXmlId = examXmlId,
                TaskXmlId = taskXmlId
            };

            var result = await _examService.GetAllExamsAsync(searchModel);

            if (!result.Any())
                return Ok(new List<TeacherTaskDtoUI>());

            return Ok(result);
        }

        [HttpGet("{studentXmlId}")]
        public async Task<IActionResult> GetResults(string studentXmlId)
        {
            var resultDtos = await _examService.GetStudentResultsAsync(studentXmlId);

            if (resultDtos.Count == 0)
                return NotFound("No exam results found");

            return Ok(resultDtos);
        }


    }
}
