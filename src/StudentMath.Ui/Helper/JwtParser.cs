namespace StudentMath.Ui.Helper
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public static class JwtParser
    {
        public static string GetRoleFromToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return "";

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "role");
            return roleClaim?.Value ?? "";
        }

        public static string GetUsernameFromToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return "";

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name" || c.Type == "name");
            return nameClaim?.Value ?? "";
        }
    }
}

