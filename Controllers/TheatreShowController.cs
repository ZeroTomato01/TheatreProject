using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;
using TheatreProject.Services;

namespace TheatreProject.Controllers
{
    [Route($"/TheatreShow")]
    public class TheatreShowController : ControllerBase
    {
        private TheatreShowService _theatreShowService;

        public TheatreShowController(TheatreShowService theatreShowService)
        {
            _theatreShowService = theatreShowService;
        }

        [HttpGet()]
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
            return await _theatreShowService.GetTheatreShows(id, title, description, location, startDate, endDate, sortBy, descending);
        }
        [HttpPost()]
        public async Task<IActionResult> PostTheatreShow([FromBody] TheatreShow theatreShow)
        {
            return await _theatreShowService.PostTheatreShow(theatreShow);
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateTheatreShow([FromBody] TheatreShow theatreShow)
        {
            return await _theatreShowService.UpdateTheatreShow(theatreShow);
        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteTheatreShow([FromQuery] int id)
        {  
            return await _theatreShowService.DeleteTheatreShow(id);
        }
    }
}