using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

[Route("api/Venue")]
[ApiController]
public class VenueController : Controller
{
    IVenueService _venueService;

    public VenueController(IVenueService venueService)
    {
        _venueService = venueService;

    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var venues = await _venueService.GetAll();
        return Ok(venues);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVenue([FromQuery] int id)
    {
        return await _venueService.GetVenue(id);
    }
    [HttpPost("")]
    protected async Task<IActionResult> PostVenue([FromBody] Venue venue)
    {
        return await _venueService.PostVenue(venue);
    }
    [HttpPut("")]
    public async Task<IActionResult> UpdateVenue([FromBody] Venue venue)
    {
        return await _venueService.UpdateVenue(venue);
    }
    [HttpDelete("")]
    public async Task<IActionResult> DeleteVenue([FromQuery] int id)
    {  
        return await _venueService.DeleteVenue(id);
    }

  

}