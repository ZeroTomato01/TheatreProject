using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Utils;

namespace TheatreProject.Services;

public enum LoginStatus { IncorrectPassword, IncorrectUsername, Success }

public enum ADMIN_SESSION_KEY { adminLoggedIn }

public class LoginService : ILoginService
{

    private readonly DatabaseContext _context;

    public LoginService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<LoginStatus> CheckPassword(string username, string inputPassword)
    {
        var admin = await _context.Admin.FindAsync(username);
        if (admin != null)
        {
            if (admin.Password == inputPassword) return LoginStatus.Success;
            return LoginStatus.IncorrectPassword;
        }
        return LoginStatus.IncorrectUsername;
    }
}