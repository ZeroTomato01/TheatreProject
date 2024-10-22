using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

public class HomeController : Controller
{

    private string AUTH_SESSION_KEY = "admin_login";

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY))) 
        {
            return RedirectPermanent("/Login/Login");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
