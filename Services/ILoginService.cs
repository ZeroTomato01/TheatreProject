using TheatreProject.Models;

namespace TheatreProject.Services;

public interface ILoginService
{
    public Task<LoginStatus> CheckCredentials(string username, string inputPassword);
    public Task<AdminDTO> GetAdminData(string username, string inputPassword);
}