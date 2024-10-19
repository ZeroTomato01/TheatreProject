using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;

namespace TheatreProject.Controllers
{
    //[Route("api/v1/TheatreShows")]
    //[ApiController]
    public class TheatreShowController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TheatreShowController(DatabaseContext context)
        {
            _context = context;
        }

        //[HttpGet]
        public async Task<IActionResult> GetTheatreShows(
            int? id,
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
                .Include(show => show.theatreShowDates);

            // Filter voor ID
            if (id.HasValue)
            {
                var show = await query.FirstOrDefaultAsync(s => s.TheatreShowId == id.Value);
                if (show == null)
                {
                    return NotFound("Show not found");
                }
                return Ok(show);
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
                query = query.Where(s => s.theatreShowDates.Any(d => d.DateAndTime >= startDate && d.DateAndTime <= endDate));
            }

            // Sorteer de shows op basis van de sortBy parameter
            query = sortBy switch
            {
                "Price" => descending ? query.OrderByDescending(s => s.Price) : query.OrderBy(s => s.Price),
                "Date" => descending ? query.OrderByDescending(s => s.theatreShowDates.FirstOrDefault().DateAndTime) : query.OrderBy(s => s.theatreShowDates.FirstOrDefault().DateAndTime),
                _ => descending ? query.OrderByDescending(s => s.Title) : query.OrderBy(s => s.Title),
            };

            // Voer de query uit en geef de resultaten terug
            var shows = await query.ToListAsync();
            return Ok(shows);
        }
    }
}