using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace TheatreProject.Services;

public class VenueService : IVenueService
{
    private DatabaseContext _context;

    public VenueService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Venue?> Get(int id)
    {
        var result = await _context.Venue.FindAsync(id);
        return result;
    }

    public async Task<List<Venue>> GetBatch(List<int> ids)
    {
        var result = await _context.Venue.
                                    Where(x=>ids.Contains(x.VenueId)).
                                    ToListAsync();
        return result;
    }

    public async Task<List<Venue>> GetAll()
    {
        var result = await _context.Venue.ToListAsync();
        return result;
    }

    public async Task<bool> Post(Venue venue)
    {
        if(venue is not null)
        {
            var register = await _context.Venue.FindAsync(venue.VenueId);

            if(register is not null)
            {
                return false;
            }
            else
            {
                await _context.Venue.AddAsync(venue);
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public async Task<List<bool>> PostBatch(List<Venue> venues)
    {
        var results = new List<bool> {};
        foreach (Venue venue in venues)
        {
            bool result = await Post(venue);
            results.Add(result);
        }
        return results;
    }

    public async Task<bool> Update(Venue venue)
    {
        var register = await _context.Venue.FindAsync(venue.VenueId);
        if(register is not null)
        {
            register.Capacity = venue.Capacity;
            register.Name = venue.Name;
            register.TheatreShows = venue.TheatreShows;
    
            _context.SaveChanges();

            return true;
        }
        return false;
    }

    public async Task<List<bool>> UpdateBatch(List<Venue> venues)
    {
        var results = new List<bool> {};
        foreach (Venue venue in venues)
        {
            bool result = await Update(venue);
            results.Add(result);
        }

        return results;
    }

    public async Task<bool> Delete(int id)
    {
        var register = await _context.Venue.FindAsync(id);

        if(register is not null)
        {
            _context.Remove(register);
            _context.SaveChanges();
            return true;
        }
        return false;
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