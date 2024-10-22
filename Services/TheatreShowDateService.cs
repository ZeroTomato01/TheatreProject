using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public class TheatreShowDateService : ITheatreShowDateService
{
    
    private DatabaseContext _context;

    public TheatreShowDateService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> GetTheatreShowDate(int id)
    {
        var DBTheatreShowDate = await _context.TheatreShowDate.FindAsync(id);
        if(DBTheatreShowDate is not null)
        {
            return new OkObjectResult(DBTheatreShowDate);
        }
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
        
    }
    public async Task<IActionResult> PostTheatreShowDate(TheatreShowDate theatreShowDate)
    {
        if(theatreShowDate is not null)
        {
            var DBTheatreShowDate = await _context.TheatreShowDate.FindAsync(theatreShowDate.TheatreShowDateId);
            if(DBTheatreShowDate is not null)
            {
                return new BadRequestObjectResult($"there's already a theatreShowDate with id: {theatreShowDate.TheatreShowDateId}in databse, use update instead");
            }
            else
            {
                await _context.TheatreShowDate.AddAsync(theatreShowDate);
                _context.SaveChanges();
                return new OkObjectResult($"heatreShowDate was added to database: {theatreShowDate}");
            }
        }
        else return new BadRequestObjectResult("given theatreShowDate was null");
    }
    public async Task<IActionResult> UpdateTheatreShowDate(TheatreShowDate theatreShowDate)
    {
        var DBTheatreShowDate = await _context.TheatreShowDate.FindAsync(theatreShowDate.TheatreShowDateId);
        if(DBTheatreShowDate is not null)
        {
            DBTheatreShowDate.DateAndTime = theatreShowDate.DateAndTime;
            DBTheatreShowDate.Reservations = theatreShowDate.Reservations;
            DBTheatreShowDate.TheatreShow = theatreShowDate.TheatreShow;
            _context.SaveChanges();

            return new OkObjectResult($"TheatreShowDate updated to {theatreShowDate}");
        }
        else return new BadRequestObjectResult($"no TheatreShowDate with given id: {theatreShowDate.TheatreShowDateId} was found in database");
    }
    public async Task<IActionResult> DeleteTheatreShowDate(int id)
    {
        var DBTheatreShowDate = await _context.TheatreShowDate.FindAsync(id);
        if(DBTheatreShowDate is not null)
        {
            _context.Remove(DBTheatreShowDate);
            _context.SaveChanges();
            return new OkObjectResult("TheatreShowDate deleted");
        }
        else return new BadRequestObjectResult($"no TheatreShowDate with given id: {id} was found in database");
        
    }
}