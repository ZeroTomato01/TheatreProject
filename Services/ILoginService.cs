using Microsoft.AspNetCore.Mvc;
namespace TheatreProject.Services;

public interface ILoginService
{
    public Task<LoginStatus> CheckPassword(string username, string inputPassword);
}