using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

public class VenueController : Controller
{
    VenueService _venueService;

    public VenueController(VenueService venueService)
    {
        _venueService = venueService;

    }

    public async Task<IActionResult> GetVenue([FromQuery] int id)
    {
        return await _venueService.GetVenue(id);
    }
    protected async Task<IActionResult> PostVenue([FromBody] Venue venue)
    {
        return await _venueService.PostVenue(venue);
    }
    public async Task<IActionResult> UpdateVenue([FromBody] Venue venue)
    {
        return await _venueService.UpdateVenue(venue);
    }
    public async Task<IActionResult> DeleteVenue([FromQuery] int id)
    {  
        return await _venueService.DeleteVenue(id);
    }

  

}