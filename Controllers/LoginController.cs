using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Services;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

[Route($"Login")]
public class LoginController : Controller
{

    private string AUTH_SESSION_KEY = "admin_login";
    ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    // [HttpGet()]
    // public IActionResult LoginPage()
    // {
    //     return View("Index");
    // }
    [HttpPost()]
    public async Task<IActionResult> LoginAction([FromForm]string username, [FromForm]string password)
    {

        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY))) //if there IS a AUTH_SESSION_KEY
        {
            return Unauthorized(); //use should not be getting the option to log in if theyre already logged in
        }

        var loggedInUser = await _loginService.CheckCredentials(username, password);

        switch (loggedInUser)
        {
            case LoginStatus.Success:
                HttpContext.Session.SetString(AUTH_SESSION_KEY, username);
                //return RedirectPermanent($"/Dashboard");
                return Ok();

            case LoginStatus.IncorrectPassword:
                ViewData["message"] = "Wachtwoord incorrect";
                return NotFound();

            case LoginStatus.IncorrectUsername:
                ViewData["message"] = "Username incorrect";
                return NotFound();

            default:
                return NotFound();
        }
    }

    [HttpPost("AdminData")] //except for password
    public async Task<AdminDTO> GetAdminData([FromForm] string username, [FromForm] string password) //except for password
    {
        return await _loginService.GetAdminData(username, password);
    }

    // [HttpGet("CheckLogin")]
    // public IActionResult CheckLogin()
    // {
    //     string? session_user = HttpContext.Session.GetString(AUTH_SESSION_KEY);
    //     if (session_user != null) return Ok();
    //     return BadRequest("Not logged in");
    // }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove(AUTH_SESSION_KEY);
        return Ok();
        //return RedirectPermanent("Login/ViewLoginPage");
    }
}


