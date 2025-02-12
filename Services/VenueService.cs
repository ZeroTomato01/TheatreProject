using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;
using TheatreProject.Services;

public class VenueService : IVenueService
{
    
    private DatabaseContext _context;

    public VenueService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> GetAll()
    {
        var result = await _context.Venue.ToArrayAsync();
        if (result is not null)
        {
            return new OkObjectResult(result);
        }
        else return new BadRequestObjectResult($"no threatres were found in database");
    }
    public async Task<IActionResult> GetVenue(int id)
    {
        var DBVenue = await _context.Venue.FindAsync(id);
        if(DBVenue is not null)
        {
            return new OkObjectResult(DBVenue);
        }
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
        
    }
    public async Task<IActionResult> PostVenue(Venue venue)
    {
        if(venue is not null)
        {
            var DBVenue = await _context.Venue.FindAsync(venue.VenueId);
            if(DBVenue is not null)
            {
                return new BadRequestObjectResult($"there's already a venue with id: {venue.VenueId}in databse, use update instead");
            }
            else
            {
                await _context.Venue.AddAsync(venue);
                _context.SaveChanges();
                return new OkObjectResult($"venue was added to database: {venue}");
            }
        }
        else return new BadRequestObjectResult("given venue was null");
    }
    public async Task<IActionResult> UpdateVenue(Venue venue)
    {
        var DBVenue = await _context.Venue.FindAsync(venue.VenueId);
        if(DBVenue is not null)
        {
            DBVenue.Capacity = venue.Capacity;
            DBVenue.Name = venue.Name;
            DBVenue.TheatreShowIds = venue.TheatreShowIds;
            _context.SaveChanges();

            return new OkObjectResult($"Venue updated to {venue}");
        }
        else return new BadRequestObjectResult($"no Venue with given id: {venue.VenueId} was found in database");
    }
    public async Task<IActionResult> DeleteVenue(int id)
    {
        var DBVenue = await _context.Venue.FindAsync(id);
        if(DBVenue is not null)
        {
            _context.Remove(DBVenue);
            _context.SaveChanges();
            return new OkObjectResult("venue deleted");
        }
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
        
    }

      public async Task<bool> CheckVenue(int id)
    {

        var DBVenue = await _context.Venue.FindAsync(id);
        if(DBVenue is null) return false;
        else return true;
    }

}