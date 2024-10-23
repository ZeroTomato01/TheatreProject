using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

// public class VenueController : Controller
// {
//     VenueService _venueService;

//     public VenueController(VenueService venueService)
//     {
//         _venueService = venueService;

//     }

//     public async Task<IActionResult> GetVenue([FromQuery] int id)
//     {
//         return await _venueService.GetVenue(id);
//     }
//     protected async Task<IActionResult> PostVenue([FromBody] Venue venue)
//     {
//         return await _venueService.PostVenue(venue);
//     }
//     public async Task<IActionResult> UpdateVenue([FromBody] Venue venue)
//     {
//         return await _venueService.UpdateVenue(venue);
//     }
//     public async Task<IActionResult> DeleteVenue([FromQuery] int id)
//     {  
//         return await _venueService.DeleteVenue(id);
//     }

  

// }

[Route($"{Globals.Version}/Venue")]
public class VenueController : Controller
{
    VenueService _venueService;

    public VenueController(VenueService venueService)
    {
        _venueService = venueService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _venueService.Get(id);
        if (result is not null)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [HttpGet("batch")]
    public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
    {
        var result = await _venueService.GetBatch(ids);
        return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll() // custom filters can be added
    {
        var result = await _venueService.GetAll(); //filters can be added to get all
        return Ok(result);
    }

    [HttpPost()]
    protected async Task<IActionResult> Post([FromBody] Venue venue)
    {
        bool result = await _venueService.Post(venue);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("batch")]
    protected async Task<IActionResult> PostBatch([FromBody] List<Venue> venues)
    {
        var result = await _venueService.PostBatch(venues);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got successfully posted");
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] Venue venue)
    {
        bool result =  await _venueService.Update(venue);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> UpdateBatch([FromBody] List<Venue> venues)
    {
        var result = await _venueService.UpdateBatch(venues);
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
        bool result =  await _venueService.Delete(id);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
    {
        var result = await _venueService.DeleteBatch(ids);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
        }
        return BadRequest();
    }
}

