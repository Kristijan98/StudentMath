public class AuthServiceModel
{
    public string Token { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public string StudentXmlId { get; private set; } = string.Empty;

    public bool IsLoggedIn => !string.IsNullOrEmpty(Token);

    public event Action? OnAuthStateChanged;

    public void SetAuth(string token, string role, string studentXmlId)
    {
        Token = token;
        Role = role;
        StudentXmlId = studentXmlId;
    }

    public void Logout()
    {
        Token = string.Empty;
        Role = string.Empty;
        StudentXmlId = string.Empty;
    }
}
