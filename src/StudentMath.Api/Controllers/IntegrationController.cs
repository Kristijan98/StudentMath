using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMath.Api.IntegrationModel;
using StudentMath.Api.Services;
using StudentMath.Processor.Interface;

namespace StudentMath.Api.Controllers
{
 
    [ApiController]
    [Route("api/integration")]
    public class IntegrationController : ControllerBase
    {
        private readonly IMathProcessor _mathProcessor;
        private readonly IIntegrationService _integrationService;

        public IntegrationController(IMathProcessor mathProcessor, IIntegrationService integrationService)
        {
            _mathProcessor = mathProcessor;
            _integrationService = integrationService;
        }

        [HttpPost("evaluate-xml")]
        [AllowAnonymous]
        public async Task<IActionResult> EvaluateXml([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            string xmlContent;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                xmlContent = await reader.ReadToEndAsync();
            }

            var results = await _integrationService.ProcessExamXmlAsync(xmlContent);

            return Ok(results);
        }

    }


}
