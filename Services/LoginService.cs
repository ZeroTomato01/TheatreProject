using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;


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
            if (!string.IsNullOrEmpty(inputPassword))
            {
                if (admin.Password == EncryptionHelper.EncryptPassword(inputPassword)) return LoginStatus.Success;
            }
            return LoginStatus.IncorrectPassword;
        }
        return LoginStatus.IncorrectUsername;
    }

    public async Task<object> GetAdminData(string username, string inputPassword)
    {
        if (await CheckCredentials(username, inputPassword) == LoginStatus.Success) //extra protection
        {
            var admin = await _context.Admin.FirstOrDefaultAsync(a => a.UserName == username);
            if (admin != null)
            {
                return new {
                    AdminId = admin.AdminId,
                    UserName = admin.UserName,
                    Email = admin.Email
                };
        }
        return null;
        
    }
}

