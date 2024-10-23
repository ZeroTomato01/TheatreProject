using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public class TheatreShowDateController : Controller
{
    TheatreShowDateService _theatreShowDateService;

    public TheatreShowDateController(TheatreShowDateService theatreShowDateService)
    {
        _theatreShowDateService = theatreShowDateService;
    }

    // public ViewTheatreShowDatePage()
    // {

    // }
    public async Task<IActionResult> GetTheatreShowDate([FromQuery] int id)
    {
        return await _theatreShowDateService.GetTheatreShowDate(id);
    }
    protected async Task<IActionResult> PostTheatreShowDate([FromBody] TheatreShowDate theatreShowDate)
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