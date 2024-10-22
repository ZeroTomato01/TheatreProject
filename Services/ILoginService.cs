namespace TheatreProject.Services;

public interface ILoginService
{
    public Task<LoginStatus> CheckCredentials(string username, string inputPassword);
}