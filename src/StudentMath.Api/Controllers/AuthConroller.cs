using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentMath.Api.DTOs;
using StudentMath.Api.Services;
using StudentMath.Core.Domain;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto userLogin)
    {
        var user = _authService.ValidateUser(userLogin.Username, userLogin.Password);

        if (user == null)
            return Unauthorized();

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

}

