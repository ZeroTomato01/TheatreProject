using System.Text;
using Microsoft.AspNetCore.Mvc;
using TheatreProject.Services;
using TheatreProject.Models;

namespace TheatreProject.Controllers;

[Route($"Privacy")]
public class PrivacyController : Controller
{
    [HttpGet()]
    public IActionResult PrivacyPage()
    {
        return View("Index");
    }
}