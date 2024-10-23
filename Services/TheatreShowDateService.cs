using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

public class TheatreShowDateService : ITheatreShowDateService
{
    
    private DatabaseContext _context;
    // private TheatreShowService _theatreShowService;

    public TheatreShowDateService(DatabaseContext context, TheatreShowService theatreShowService)
    {
        _context = context;
        // _theatreShowService = theatreShowService;
    }
    
    public async Task<TheatreShowDate?> Get(int id)
    {
        var result = await _context.TheatreShowDate.FindAsync(id);
        return result;
    }



    public async Task<List<TheatreShowDate>> GetBatch(List<int> ids)
    {
        var result = await _context.TheatreShowDate.
                                    Where(x=>ids.Contains(x.TheatreShowDateId)).
                                    ToListAsync();
        return result;
    }



    public async Task<List<TheatreShowDate>> GetAll()
    {
        var result = await _context.TheatreShowDate.ToListAsync();
        return result;
    }
    
    public async Task<bool> Post(TheatreShowDate theatreShowDate)
    {
        if(theatreShowDate is not null)
        {
            var register = await _context.TheatreShowDate.FindAsync(theatreShowDate.TheatreShowDateId);

            if(register is not null)
            {
                return false;
            }
            else
            {
                await _context.TheatreShowDate.AddAsync(theatreShowDate);
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public async Task<List<bool>> PostBatch(List<TheatreShowDate> theatreShowDates)
    {
        var results = new List<bool> {};
        foreach (TheatreShowDate theatreShowDate in theatreShowDates)
        {
            bool result = await Post(theatreShowDate);
            results.Add(result);
        }
        return results;
    }

    public async Task<bool> Update(TheatreShowDate theatreShowDate)
    {
        var register = await _context.TheatreShowDate.FindAsync(theatreShowDate.TheatreShowDateId);
        if(register is not null)
        {
            register.DateAndTime = theatreShowDate.DateAndTime;
            register.Reservations = theatreShowDate.Reservations;
            register.TheatreShow = theatreShowDate.TheatreShow;
            _context.SaveChanges();

            return true;
        }
        else return false;
    }

    public async Task<List<bool>> UpdateBatch(List<TheatreShowDate> theatreShowDates)
    {
        var results = new List<bool> {};
        foreach (TheatreShowDate theatreShowDate in theatreShowDates)
        {
            bool result = await Update(theatreShowDate);
            results.Add(result);
        }

        return results;
    }

    public async Task<bool> Delete(int id)
    {
        var register = await _context.TheatreShowDate.FindAsync(id);
        if(register is not null)
        {
            _context.Remove(register);
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

    // public async Task<bool> CheckTheatreShowDate(int id)
    // {
    //     var DBtheatreShowDate = await _context.TheatreShowDate.FindAsync(id);
    //     if(DBtheatreShowDate is null) return false;
    //     if(DBtheatreShowDate.TheatreShow is null) return false;
    //     if(DBtheatreShowDate.TheatreShow.TheatreShowId is 0) return false;
    //     else return await _theatreShowService.CheckTheatreShow(DBtheatreShowDate.TheatreShow.TheatreShowId);
    // }
}