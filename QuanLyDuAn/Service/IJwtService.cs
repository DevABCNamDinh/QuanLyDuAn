using System.Security.Claims;

namespace QuanLyDuAn.Service
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        ClaimsPrincipal ValidateToken(string token);
    }
}
