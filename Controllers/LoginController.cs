using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Services;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

[Route("Login")]
public class LoginController : Controller
{

    private string AUTH_SESSION_KEY = "admin_login";
    ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    //call in the view (_Layout.cshtml) using asp-controller="Login" and asp-action="ViewLoginPage"
    //or using "Login/ViewLoginPage"
    //[HttpGet("api/ViewLoginPage")]
    [Route("")]
    public IActionResult ViewLoginPage() 
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY)))
        {
            return RedirectPermanent($"/{Globals.Version}/Home/Dashboard");
        }
        return View("Login");
    }



    //[HttpPost("api/LoginAction")] //this attribute isn't necessary for instances where "method" is specified
    //like the call in View(Login.cshtml) using action="Login" and method="LoginAction"
    //but it IS (seemingly) necessary for instances where "method" can't be specified, like in _Layout.cshtml
    //for example Logout() being called in _Layout.cshtml doesn't work without an attribute as no method is specified
    [HttpPost("Login/LoginAction")]
    [HttpPost("/LoginAction")] //im not sure if the Route of the LoginController gets prepended to this or not
    public async Task<IActionResult> LoginAction([FromForm] string username, [FromForm] string password)
    {

        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY)))
        {
            return RedirectPermanent($"/{Globals.Version}/Home/Dashboard");
        }

        var loggedInUser = await _loginService.CheckCredentials(username, password);

        switch (loggedInUser)
        {
            case LoginStatus.Success:
                HttpContext.Session.SetString(AUTH_SESSION_KEY, username);
                return RedirectPermanent($"/{Globals.Version}/Home/Dashboard");

            case LoginStatus.IncorrectPassword:
                ViewData["message"] = "Wachtwoord incorrect";
                return View("Login");

            case LoginStatus.IncorrectUsername:
                ViewData["message"] = "Username incorrect";
                return View("Login");

            default:
                return View("Login");
        }
    }

    [HttpGet("api/CheckLogin")]
    public IActionResult CheckLogin()
    {
        string? session_user = HttpContext.Session.GetString(AUTH_SESSION_KEY);
        if (session_user != null) return Ok();
        return BadRequest("Not logged in");
    }

    [HttpGet("api/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove(AUTH_SESSION_KEY);
        return View("Login");
        //return RedirectPermanent("Login/ViewLoginPage");
    }
}


