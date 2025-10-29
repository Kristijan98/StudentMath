using StudentMath.Api.DTOs;
using StudentMath.Core.Domain;

namespace StudentMath.Api.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(InMemoryUser user);
        InMemoryUser ValidateUser(string username, string password);
    }

}
