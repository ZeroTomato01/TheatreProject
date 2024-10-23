using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

namespace TheatreProject.Controllers;

// [Route($"{Globals.Version}/TheatreShow")]
// public class TheatreShowController : ControllerBase
// {
//     private TheatreShowService _theatreShowService;

//     public TheatreShowController(TheatreShowService theatreShowService)
//     {
//         _theatreShowService = theatreShowService;
//     }

//     [HttpGet()]
//     public async Task<IActionResult> GetTheatreShows(
//         int? id,
//         string? title,
//         string? description,
//         string? location,
//         DateTime? startDate,
//         DateTime? endDate,
//         string? sortBy = "Title",
//         bool descending = false)
//     {
//         return await _theatreShowService.GetTheatreShows(id, title, description, location, startDate, endDate, sortBy, descending);
//     }
//     [HttpPost()]
//     protected async Task<IActionResult> PostTheatreShow([FromBody] TheatreShow theatreShow)
//     {
//         return await _theatreShowService.PostTheatreShow(theatreShow);
//     }
//     [HttpPut()]
//     public async Task<IActionResult> UpdateTheatreShow([FromBody] TheatreShow theatreShow)
//     {
//         return await _theatreShowService.UpdateTheatreShow(theatreShow);
//     }
//     [HttpDelete()]
//     public async Task<IActionResult> DeleteTheatreShow([FromQuery] int id)
//     {  
//         return await _theatreShowService.DeleteTheatreShow(id);
//     }
// }

[Route($"{Globals.Version}/TheatreShow")]
public class TheatreShowController : Controller
{
    TheatreShowService _theatreShowService;

    public TheatreShowController(TheatreShowService theatreShowService)
    {
        _theatreShowService = theatreShowService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _theatreShowService.Get(id);
        if (result is not null)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [HttpGet("batch")]
    public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
    {
        var result = await _theatreShowService.GetBatch(ids);
        return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll() // custom filters can be added
    {
        var result = await _theatreShowService.GetAll(); //filters can be added to get all
        return Ok(result);
    }

    [HttpPost()]
    protected async Task<IActionResult> Post([FromBody] TheatreShow theatreShow)
    {
        bool result = await _theatreShowService.Post(theatreShow);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("batch")]
    protected async Task<IActionResult> PostBatch([FromBody] List<TheatreShow> theatreShows)
    {
        var result = await _theatreShowService.PostBatch(theatreShows);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got successfully posted");
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] TheatreShow theatreShow)
    {
        bool result =  await _theatreShowService.Update(theatreShow);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> UpdateBatch([FromBody] List<TheatreShow> theatreShows)
    {
        var result = await _theatreShowService.UpdateBatch(theatreShows);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got succesfully updated");
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {  
        bool result =  await _theatreShowService.Delete(id);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
    {
        var result = await _theatreShowService.DeleteBatch(ids);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
        }
        return BadRequest();
    }
}


