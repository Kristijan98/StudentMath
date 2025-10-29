using StudentMath.Api.IntegrationModel;

namespace StudentMath.Api.Services
{
    public interface IIntegrationService
    {
        Task<IntegrationTeacherDto> ProcessExamXmlAsync(string xmlContent);
    }
}
