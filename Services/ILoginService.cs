using TheatreProject.Models;

namespace TheatreProject.Services;

public interface ILoginService
{
    public Task<Admin> CheckCredentials(string username, string inputPassword);
}