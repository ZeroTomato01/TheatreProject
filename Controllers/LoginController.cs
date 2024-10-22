using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Services;
using TheatreProject.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace TheatreProject.Controllers;

[Route($"{Globals.Version}/Login")]
public class LoginController : Controller
{

    private string AUTH_SESSION_KEY = "admin_login";
    LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    //call in the view (_Layout.cshtml) using asp-controller="Login" and asp-action="ViewLoginPage"
    //or using "Login/ViewLoginPage"
    [HttpGet("ViewLoginPage")]
    public IActionResult ViewLoginPage() 
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY)))
        {
            return RedirectPermanent("/Home/Dashboard");
        }
        return View("Login");
    }



    [HttpPost("api/LoginAction")] //this attribute isn't necessary for instances where "method" is specified
    //like the call in View(Login.cshtml) using action="Login" and method="LoginAction"
    //but it IS (seemingly) necessary for instances where "method" can't be specified, like in _Layout.cshtml
    //for example Logout() being called in _Layout.cshtml doesn't work without an attribute as no method is specified
    
    public IActionResult LoginAction([FromForm] string username, [FromForm] string password)
    {

        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY)))
        {
            return RedirectPermanent("/Home/Dashboard");
        }

        var loggedInUser = await _loginService.CheckPassword(username, password);

        switch (loggedInUser)
        {
            case LoginStatus.Success:
                HttpContext.Session.SetString(AUTH_SESSION_KEY, username);
                return RedirectPermanent("/Home/Dashboard");

            case LoginStatus.IncorrectPassword:
                ViewData["message"] = "Wachtwoord incorrect";
                return View("Login");

            case LoginStatus.IncorrectUsername:
                ViewData["message"] = "username incorrect";
                return View("Login");

            default:
                return View("Login");
        }

        // if (loggedInUser == LoginStatus.IncorrectPassword) commented for weird merge conflict
        
        // {
        //     ViewData["message"] = "Wachtwoord incorrect";
        //     return View("Login");
        // }

        // if (loggedInUser == LoginStatus.IncorrectUsername)
        // {
        //     ViewData["message"] = "username incorrect";
        //     return View("Login");
        // }

        // return View("Login");

    }

    [HttpGet("api/CheckLogin")]
    public IActionResult CheckLogin()
    {
        string? session_user = HttpContext.Session.GetString(AUTH_SESSION_KEY);

        return Json(new LoginResponseBody
        {
            User = session_user,
            IsLoggedIn = !string.IsNullOrEmpty(session_user)
        });
    }

    [HttpGet("api/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove(AUTH_SESSION_KEY);
        return View("Login");
        //return RedirectPermanent("Login/ViewLoginPage");
    }



    //[HttpPost("/api/login/admin")] //this method wasn't ever used so im not sure what to change it to
    public IActionResult LoginAdmin([FromBody] LoginBody loginBody)
    {

        // Check if we are logged in
        var loggedInUser = HttpContext.Session.GetString(AUTH_SESSION_KEY);
        if (!string.IsNullOrEmpty(loggedInUser))
        {
            // Yes, we are logged in
            return Json(new LoginResponseBody
            {
                User = loggedInUser,
                IsLoggedIn = true
            });
        }

        // Check if password is correct
        if (loginBody.Username == "admin" && loginBody.Password == "admin")
        {
            HttpContext.Session.SetString(AUTH_SESSION_KEY, loginBody.Username);

            return Json(new LoginResponseBody
            {
                User = loginBody.Username,
                IsLoggedIn = true
            });
        }
        // Password or username incorrect
        return Json(new LoginResponseBody
        {
            User = null,
            IsLoggedIn = false
        });
    }
}


