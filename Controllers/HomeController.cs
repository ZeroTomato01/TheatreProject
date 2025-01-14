using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

//[Route($"/{Globals.Version}/Home")]
[Route("")]
public class HomeController : Controller
{

    private string AUTH_SESSION_KEY = "admin_login";
    
    [Route("")]
    [Route("Index")]
    //[Route($"/{Globals.Version}/Home/Index")]
    public IActionResult Index()
    {
        return View();
    }
    [Route("Privacy")]
    //[Route($"/{Globals.Version}/Home/Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("Dashboard")]
    //[Route($"/{Globals.Version}/Home/Dashboard")]
    public IActionResult Dashboard()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY))) 
        {
            return RedirectPermanent($"{Globals.Version}/Login/ViewLoginPage");
        }

        return View();
    }

    [Route("Error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
