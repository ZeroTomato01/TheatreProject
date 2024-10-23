using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;

namespace TheatreProject.Services;

public class TheatreShowService : ITheatreShowService
{
    private readonly DatabaseContext _context;

    public TheatreShowService(DatabaseContext context, IVenueService venueService)
    {
        _context = context;
    }

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

    public async Task<List<TheatreShow>> GetAll(string? title = null, double? maxPrice = null, int? venueId = null)
    {
        var query = _context.TheatreShow.AsQueryable();
        if (title != null)
        {
            query.Where(x => x.Title == title);
        }
        if (maxPrice != null)
        {
            query.Where(x => x.Price <= maxPrice);
        }
        if (venueId != null)
        {
            query.Where(x => x.Venue != null && x.Venue.VenueId == venueId);
        }

        // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

        var result = await query.ToListAsync();

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
}