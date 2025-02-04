using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;


namespace TheatreProject.Services;

public class LoginService : ILoginService
{

    private readonly DatabaseContext _context;

    public LoginService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Admin> CheckCredentials(string username, string inputPassword)
    {
        var admin = await _context.Admin.FirstOrDefaultAsync(a => a.UserName == username);
        if (admin != null)
        {
            if (!string.IsNullOrEmpty(inputPassword))
            {
                var debugstep = EncryptionHelper.EncryptPassword(inputPassword);
                if (admin.Password == debugstep) return admin;
            }
        }
        return null;
    }
}

