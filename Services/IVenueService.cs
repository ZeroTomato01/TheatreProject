using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Services;

public interface IVenueService
{
    public Task<IActionResult> GetVenue(int id);

    public Task<IActionResult> PostVenue(Venue venue);

    public Task<IActionResult> UpdateVenue(Venue venue);

    public Task<IActionResult> DeleteVenue(int id);
}