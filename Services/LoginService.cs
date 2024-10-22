using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<LoginStatus> CheckCredentials(string username, string inputPassword)
    {
        var admin = await _context.Admin.FirstOrDefaultAsync(a => a.UserName == username);
        if (admin != null)
        {
            if (admin.Password == EncryptionHelper.EncryptPassword(inputPassword)) return LoginStatus.Success;
            return LoginStatus.IncorrectPassword;
        }
        return LoginStatus.IncorrectUsername;
    }
}