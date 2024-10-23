using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using TheatreProject.Services;

namespace TheatreProject.Controllers;

// [Route("Customer")]
// public class CustomerController : Controller
// {
//     CustomerService _customerService;

//     public CustomerController(CustomerService customerService)
//     {
//         _customerService = customerService;
//     }

//     [HttpGet()]
//     public async Task<IActionResult> GetCustomer([FromQuery] int id)
//     {
//         Customer DBcustomer = await _customerService.Get(id);
//         if(DBcustomer is null) return BadRequest($"Customer with id {id} couldn't be found");
//         else return Ok();
//     }
//     [HttpPost()]
//     protected async Task<IActionResult> PostCustomer([FromBody] Customer customer)
//     {
//         bool success = await _customerService.Post(customer);
//         if(success) return Ok("Customer posted");
//         else return BadRequest("Customer failed to post");

//     }
//     [HttpPut()]
//     public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
//     {
//         bool success =  await _customerService.Update(customer);
//         if(success) return Ok("Customer posted");
//         else return BadRequest("Customer fiailed to update");
//     }
//     [HttpDelete()]
//     public async Task<IActionResult> DeleteCustomer([FromQuery] int id)
//     {  
//         bool success = await _customerService.Delete(id);
//         if(success) return Ok("Customer posted");
//         else return BadRequest($"Customer with id: {id} could not be deleted");
//     }
// }

[Route($"{Globals.Version}/Customer")]
public class CustomerController : Controller
{
    CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _customerService.Get(id);
        if (result is not null)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    [HttpGet("batch")]
    public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
    {
        var result = await _customerService.GetBatch(ids);
        return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll() // custom filters can be added
    {
        var result = await _customerService.GetAll(); //filters can be added to get all
        return Ok(result);
    }

    [HttpPost()]
    protected async Task<IActionResult> Post([FromBody] Customer customer)
    {
        bool result = await _customerService.Post(customer);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("batch")]
    protected async Task<IActionResult> PostBatch([FromBody] List<Customer> customers)
    {
        var result = await _customerService.PostBatch(customers);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got successfully posted");
        }
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] Customer customer)
    {
        bool result =  await _customerService.Update(customer);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> UpdateBatch([FromBody] List<Customer> customers)
    {
        var result = await _customerService.UpdateBatch(customers);
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
        bool result =  await _customerService.Delete(id);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
    {
        var result = await _customerService.DeleteBatch(ids);
        if (result.Contains(true))
        {
            int trueCount = result.Count(x => x == true);
            return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
        }
        return BadRequest();
    }
}


