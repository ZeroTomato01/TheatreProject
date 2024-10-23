using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

// public class TheatreShowDateController : Controller
// {
//     TheatreShowDateService _theatreShowDateService;

//     public TheatreShowDateController(TheatreShowDateService theatreShowDateService)
//     {
//         _theatreShowDateService = theatreShowDateService;
//     }

//     public async Task<IActionResult> GetTheatreShowDate([FromQuery] int id)
//     {
//         return await _theatreShowDateService.GetTheatreShowDate(id);
//     }
//     protected async Task<IActionResult> PostTheatreShowDate([FromBody] TheatreShowDate theatreShowDate)
//     {
//         return await _theatreShowDateService.PostTheatreShowDate(theatreShowDate);
//     }
//     public async Task<IActionResult> UpdateTheatreShowDate([FromBody] TheatreShowDate theatreShowDate)
//     {
//         return await _theatreShowDateService.UpdateTheatreShowDate(theatreShowDate);
//     }
//     public async Task<IActionResult> DeleteTheatreShowDate([FromQuery] int id)
//     {  
//         return await _theatreShowDateService.DeleteTheatreShowDate(id);
//     }

// }

[Route($"{Globals.Version}/TheatreShowDate")]
public class TheatreShowDateController : Controller
{
    TheatreShowDateService _theatreShowDateService;

    public TheatreShowDateController(TheatreShowDateService theatreShowDateService)
    {
        _theatreShowDateService = theatreShowDateService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _theatreShowDateService.Get(id);
        if (result is not null)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [HttpGet("batch")]
    public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
    {
        var result = await _theatreShowDateService.GetBatch(ids);
        return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll() // custom filters can be added
    {
        var result = await _theatreShowDateService.GetAll(); //filters can be added to get all
        return Ok(result);
    }

    [HttpPost()]
    protected async Task<IActionResult> Post([FromBody] TheatreShowDate theatreShowDate)
    {
        bool result = await _theatreShowDateService.Post(theatreShowDate);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("batch")]
    protected async Task<IActionResult> PostBatch([FromBody] List<TheatreShowDate> theatreShowDates)
    {
        var result = await _theatreShowDateService.PostBatch(theatreShowDates);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got successfully posted");
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] TheatreShowDate theatreShowDate)
    {
        bool result =  await _theatreShowDateService.Update(theatreShowDate);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> UpdateBatch([FromBody] List<TheatreShowDate> theatreShowDates)
    {
        var result = await _theatreShowDateService.UpdateBatch(theatreShowDates);
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
        bool result =  await _theatreShowDateService.Delete(id);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
    {
        var result = await _theatreShowDateService.DeleteBatch(ids);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
        }
        return BadRequest();
    }
}

