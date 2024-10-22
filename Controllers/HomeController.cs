using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

[Route("Home")]
public class HomeController : Controller
{

    private string AUTH_SESSION_KEY = "admin_login";
    
    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }
    
    [Route("Index")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("Dashboard")]
    public IActionResult Dashboard()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString(AUTH_SESSION_KEY))) 
        {
            return RedirectPermanent("/Login/ViewLoginPage");
        }

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
