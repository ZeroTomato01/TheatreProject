using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Services;
using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace TheatreProject.Controllers;

[Route($"Login")]
public class LoginController : Controller
{

    ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var admin = await _loginService.CheckCredentials(loginRequest.Username, loginRequest.Password);

        if (admin == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        var adminDataDTO = new AdminDTO
        {
            AdminId = admin.AdminId,
            UserName = admin.UserName,
            Email = admin.Email
        };

        return Ok(adminDataDTO);
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}


