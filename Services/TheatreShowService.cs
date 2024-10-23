using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;

namespace TheatreProject.Services;
public class TheatreShowService : ITheatreShowService
{
    private readonly DatabaseContext _context;
    // private IVenueService _venueService;

        public TheatreShowService(DatabaseContext context, IVenueService venueService)
        {
            _context = context;
            // _venueService = venueService;
        }
    // public async Task<IActionResult> GetTheatreShows(int? id,
    //         string? title,
    //         string? description,
    //         string? location,
    //         DateTime? startDate,
    //         DateTime? endDate,
    //         string? sortBy = "Title",
    //         bool descending = false)
    // {
    //     IQueryable<TheatreShow> query = _context.Set<TheatreShow>()
    //             .Include(show => show.Venue)
    //             .Include(show => show.theatreShowDates);

    //     // Filter voor ID
    //     if (id.HasValue)
    //     {
    //         var show = await query.FirstOrDefaultAsync(s => s.TheatreShowId == id.Value);
    //         if (show == null)
    //         {
    //             return new NotFoundObjectResult("Show not found");
    //         }
    //         return new OkObjectResult(show);

    //         //return Ok(show);
    //     }

    //     // Filter voor titel of beschrijving
    //     if (!string.IsNullOrEmpty(title))
    //     {
    //         query = query.Where(s => s.Title != null && s.Title.Contains(title));
    //     }
    //     if (!string.IsNullOrEmpty(description))
    //     {
    //         query = query.Where(s => s.Description != null && s.Description.Contains(description));
    //     }

    //     // Filter voor locatie (locatie naam)
    //     if (!string.IsNullOrEmpty(location))
    //     {
    //         query = query.Where(s => s.Venue != null && s.Venue.Name.Contains(location));
    //     }

    //     // Filter de shows op basis van start en eind datum
    //     if (startDate.HasValue && endDate.HasValue)
    //     {
    //         query = query.Where(s => s.theatreShowDates.Any(d => d.DateAndTime >= startDate && d.DateAndTime <= endDate));
    //     }

    //     // Sorteer de shows op basis van de sortBy parameter
    //     query = sortBy switch
    //     {
    //         "Price" => descending ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
    //         "Date" => descending ? query.OrderByDescending(s => s.theatreShowDates.FirstOrDefault().DateAndTime) : query.OrderBy(s => s.theatreShowDates.FirstOrDefault().DateAndTime),
    //         _ => descending ? query.OrderByDescending(s => s.Title) : query.OrderBy(s => s.Title),
    //     };

    //     // Voer de query uit en geef de resultaten terug
    //     var shows = await query.ToListAsync();
    //     return new OkObjectResult(shows);
    // }

    public async Task<TheatreShow?> Get(int id)
    {
        var result = await _context.TheatreShow.FindAsync(id);
        return result;
    }

    public async Task<List<TheatreShow>> GetBatch(List<int> ids)
    {
        var result = await _context.TheatreShow.
                                    Where(x=>ids.Contains(x.TheatreShowId)).
                                    ToListAsync();
        return result;
    }

    public async Task<List<T>> GetAll()
    {
        var result = await _context.TheatreShow.ToListAsync();
        return result;
    }

    public async Task<bool> Post(TheatreShow theatreShow)
    {
        if(theatreShow is not null)
        {
            var register = await _context.TheatreShow.FindAsync(theatreShow.TheatreShowId);

            if(register is not null)
            {
                return false;
            }
            else
            {
                await _context.TheatreShow.AddAsync(theatreShow);
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public async Task<List<bool>> PostBatch(List<TheatreShow> theatreShows)
    {
        var results = new List<bool> {};
        foreach (TheatreShow theatreShow in theatreShows)
        {
            bool result = await Post(theatreShow);
            results.Add(result);
        }
        return results;
    }

    public async Task<bool> Update(TheatreShow theatreShow)
    {
        var DBShow =await _context.TheatreShow.FindAsync(theatreShow.TheatreShowId);
        if(DBShow is not null)
        {
            DBShow.Venue = theatreShow.Venue;
            DBShow.Title = theatreShow.Title;
            DBShow.theatreShowDates = theatreShow.theatreShowDates;
            DBShow.Price = theatreShow.Price;
            DBShow.Description = theatreShow.Description;
            //DBShow = theatreShow;
            _context.SaveChanges();

            return true;
        }
        else return false;
        
    }

    public async Task<List<bool>> UpdateBatch(List<TheatreShow> theatreShows)
    {
        var results = new List<bool> {};
        foreach (TheatreShow theatreShow in theatreShows)
        {
            bool result = await Update(theatreShow);
            results.Add(result);
        }

        return results;
    }
    
    public async Task<bool> Delete(int id)
    {  
        var register = await _context.TheatreShow.FindAsync(id);
        if(register is not null)
        {
            _context.TheatreShow.Remove(register);
            _context.SaveChanges();
            return true;
        }
        
        else return false;
    }

    public async Task<List<bool>> DeleteBatch(List<int> ids)
    {
        var results = new List<bool> {};
        foreach (int id in ids)
        {
            bool result = await Delete(id);
            results.Add(result);
        }
        return results;
    }
    
    // public async Task<bool> CheckTheatreShow(int id)
    // {
    //     var DBtheatreShow = await _context.TheatreShow.FindAsync(id);
    //     if(DBtheatreShow is null) return false;
    //     if(DBtheatreShow.Venue is null) return false;
    //     if(DBtheatreShow.Venue.VenueId is 0) return false;
    //     else return await _venueService.CheckVenue(DBtheatreShow.Venue.VenueId);
    // }
}