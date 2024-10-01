using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Services;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private string AUTH_SESSION_KEY = "admin_login";

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY)))
        {
            return RedirectPermanent("/Home/Dashboard");
        }
        return View();
    }

    [HttpPost("/login/admin")]
    public IActionResult LoginAdmin([FromForm] string username, [FromForm] string password)
    {

        if (!string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY)))
        {
            return RedirectPermanent("/Home/Dashboard");
        }

        if (username == "admin" && password == "admin")
        {
            HttpContext.Session.SetString(AUTH_SESSION_KEY, username);

            return RedirectPermanent("/Home/Dashboard");
        }
        ViewData["message"] = "Wachtwoord of gebruikersnaam incorrect";
        return View("Login");
    }

    [HttpGet("/api/login/check")]
    public IActionResult CheckLogin()
    {
        string? session_user = HttpContext.Session.GetString(AUTH_SESSION_KEY);

        return Json(new LoginResponseBody
        {
            User = session_user,
            IsLoggedIn = !string.IsNullOrEmpty(session_user)
        });
    }

    [HttpGet("/login/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove(AUTH_SESSION_KEY);
        return View("Login");
    }



    [HttpPost("/api/login/admin")]
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


