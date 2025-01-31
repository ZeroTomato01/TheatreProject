using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

[Route($"/TheatreShowDate")]
public class TheatreShowDateController : Controller
{
    ITheatreShowDateService _theatreShowDateService;

    public TheatreShowDateController(ITheatreShowDateService theatreShowDateService)
    {
        _theatreShowDateService = theatreShowDateService;
    }

    // public ViewTheatreShowDatePage()
    // {

    // }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        return await _theatreShowDateService.GetAll();
    }
    [HttpGetAttribute("future")]
    public async Task<IActionResult> GetAllFuture()
    {
        return await _theatreShowDateService.GetAllFuture();
    }
    public async Task<IActionResult> GetTheatreShowDate([FromQuery] int id)
    {
        return await _theatreShowDateService.GetTheatreShowDate(id);
    }
    public async Task<IActionResult> PostTheatreShowDate([FromBody] TheatreShowDate theatreShowDate)
    {
        return await _theatreShowDateService.PostTheatreShowDate(theatreShowDate);
    }
    public async Task<IActionResult> UpdateTheatreShowDate([FromBody] TheatreShowDate theatreShowDate)
    {
        return await _theatreShowDateService.UpdateTheatreShowDate(theatreShowDate);
    }
    public async Task<IActionResult> DeleteTheatreShowDate([FromQuery] int id)
    {  
        return await _theatreShowDateService.DeleteTheatreShowDate(id);
    }

}