using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

[Route("Customer")]
public class CustomerController : Controller
{
    CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetCustomer([FromQuery] int id)
    {
        Customer DBcustomer = await _customerService.Get(id);
        if(DBcustomer is null) return BadRequest($"Customer with id {id} couldn't be found");
        else return Ok();
    }
    [HttpPost()]
    protected async Task<IActionResult> PostCustomer([FromBody] Customer customer)
    {
        bool success = await _customerService.Post(customer);
        if(success) return Ok("Customer posted");
        else return BadRequest("Customer failed to post");

    }
    [HttpPut()]
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        bool success =  await _customerService.Update(customer);
        if(success) return Ok("Customer posted");
        else return BadRequest("Customer fiailed to update");
    }
    [HttpDelete()]
    public async Task<IActionResult> DeleteCustomer([FromQuery] int id)
    {  
        bool success = await _customerService.Delete(id);
        if(success) return Ok("Customer posted");
        else return BadRequest($"Customer with id: {id} could not be deleted");
    }
}