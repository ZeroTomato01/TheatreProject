using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;

namespace TheatreProject.Services;
public class TheatreShowService : ITheatreShowService
{
    private readonly DatabaseContext _context;
    private IVenueService _venueService;

        public TheatreShowService(DatabaseContext context, IVenueService venueService)
        {
            _context = context;
            _venueService = venueService;
        }

    public async Task<IActionResult> GetAll()
    {
        var allShows = await _context.TheatreShow.ToArrayAsync();
        allShows.OrderBy(x => x.Title);
        return  new OkObjectResult(allShows);
    }

    public async Task<IActionResult> GetTheatreShows(int? id,
            string? title,
            string? description,
            string? location,
            DateTime? startDate,
            DateTime? endDate,
            string? sortBy = "Title",
            bool descending = false)
    {
        IQueryable<TheatreShow> query = _context.Set<TheatreShow>()
                .Include(show => show.Venue)
                .Include(show => show.TheatreShowDates);

        // Filter voor ID
        if (id.HasValue)
        {
            var show = await query.FirstOrDefaultAsync(s => s.TheatreShowId == id.Value);
            if (show == null)
            {
                return new NotFoundObjectResult("Show not found");
            }
            return new OkObjectResult(show);

            //return Ok(show);
        }

        // Filter voor titel of beschrijving
        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(s => s.Title != null && s.Title.Contains(title));
        }
        if (!string.IsNullOrEmpty(description))
        {
            query = query.Where(s => s.Description != null && s.Description.Contains(description));
        }

        // Filter voor locatie (locatie naam)
        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(s => s.Venue != null && s.Venue.Name.Contains(location));
        }

        // Filter de shows op basis van start en eind datum
        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(s => s.TheatreShowDates.Any(d => d.DateAndTime >= startDate && d.DateAndTime <= endDate));
        }

        // Sorteer de shows op basis van de sortBy parameter
        query = sortBy switch
        {
            "Price" => descending ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
            "Date" => descending ? query.OrderByDescending(s => s.TheatreShowDates.FirstOrDefault().DateAndTime) : query.OrderBy(s => s.TheatreShowDates.FirstOrDefault().DateAndTime),
            _ => descending ? query.OrderByDescending(s => s.Title) : query.OrderBy(s => s.Title),
        };

        // Voer de query uit en geef de resultaten terug
        var shows = await query.ToListAsync();
        return new OkObjectResult(shows);
    }
    public async Task<IActionResult> PostTheatreShow(TheatreShow theatreShow)
    {
        if(theatreShow is not null)
        {
            var DBTheatreShow = await _context.TheatreShow.FindAsync(theatreShow.TheatreShowId);
            if(DBTheatreShow is not null)
            {
                return new BadRequestObjectResult($"there's already a TheatreShow with id: {theatreShow.TheatreShowId}in databse, use update instead");
            }
            else
            {
                await _context.TheatreShow.AddAsync(theatreShow);
                _context.SaveChanges();
                return new OkObjectResult($"theatreshow was added to database: {theatreShow}");
            }
        }
        else return new BadRequestObjectResult("given theatreshow was null");
    }
    public async Task<IActionResult> UpdateTheatreShow(TheatreShow theatreShow)
    {
        var DBShow =await _context.TheatreShow.FindAsync(theatreShow.TheatreShowId);
        if(DBShow is not null)
        {
            DBShow.Venue = theatreShow.Venue;
            DBShow.Title = theatreShow.Title;
            DBShow.TheatreShowDates = theatreShow.TheatreShowDates;
            DBShow.Price = theatreShow.Price;
            DBShow.Description = theatreShow.Description;
            //DBShow = theatreShow;
            _context.SaveChanges();

            return new OkObjectResult($"Theatre updated to {theatreShow}");
        }
        else return new BadRequestObjectResult($"no threatre with given id: {theatreShow.TheatreShowId} was found in database");
        
    }
    public async Task<IActionResult> DeleteTheatreShow(int id)
    {  
        var DBShow = await _context.TheatreShow.FindAsync(id);
        if(DBShow is not null)
        {
            _context.TheatreShow.Remove(DBShow);
            _context.SaveChanges();
            return new OkObjectResult("Theatre deleted");
        }
        
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
    }

    public async Task<bool> CheckTheatreShow(int id)
    {
        var DBtheatreShow = await _context.TheatreShow.FindAsync(id);
        if(DBtheatreShow is null) return false;
        if(DBtheatreShow.Venue is null) return false;
        if(DBtheatreShow.Venue.VenueId is 0) return false;
        else return await _venueService.CheckVenue(DBtheatreShow.Venue.VenueId);
    }
}