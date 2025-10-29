using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentMath.Api.Services;
using StudentMath.Core.Domain;
using StudentMath.Core.Interface;
using StudentMath.Data;
using StudentMath.Processor.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentMathDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IStudentMathRepository, StudentMathRepository>();

builder.Services.AddScoped<IMathProcessor, MathProcessor>();
builder.Services.AddScoped<IXmlExamParser, XmlExamParser>();
builder.Services.AddScoped<ExamProcessingService>();
builder.Services.AddScoped<IIntegrationService, IntegrationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExamInterface, ExamService>();


var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"] ?? "ThisIsASuperSecretKey1234567890!!!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();


UserStore.Users.Clear(); 
UserStore.Users.AddRange(new[]
{
    new InMemoryUser { Username = "teacher1", Password = "teacher", Role = "Teacher" },

    new InMemoryUser { Username = "S001", Password = "student", Role = "Student" },
    new InMemoryUser { Username = "S010", Password = "student", Role = "Student" },
    new InMemoryUser { Username = "S003", Password = "student", Role = "Student" },
    new InMemoryUser { Username = "S004", Password = "student", Role = "Student" },
    new InMemoryUser { Username = "S005", Password = "student", Role = "Student" }
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Urls.Add("https://localhost:5005");

app.Run();
