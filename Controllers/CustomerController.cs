using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

[Route("Customer")]
public class CustomerController : Controller
{
    ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetCustomer([FromQuery] int id)
    {
        Customer DBcustomer = await _customerService.Get(id);
        if(DBcustomer is null) return BadRequest($"Customer with id {id} couldn't be found");
        else return Ok(DBcustomer);
    }
    [HttpPost("Register")]
    public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
    {
        bool succes;
        if (customer.CustomerId == 0)
        {
            int randomId = new Random().Next();
            Customer? exists = await _customerService.Get(randomId);
            if (exists is null)
            {
                customer.CustomerId = randomId;
                succes = await _customerService.Post(customer);
                if(succes) return Ok("Customer posted");
                else return BadRequest("Customer failed to post");
            }
    
        }

        succes = await _customerService.Post(customer);
        if(succes) return Ok("Customer posted");
        else return BadRequest("Customer failed to post");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginCustomer([FromBody] Customer customer)
    {
        Customer DBcustomer = await _customerService.GetByMail(customer);
        if(DBcustomer is null) return BadRequest($"Customer with mail {customer.Email} couldn't be found");
        else return Ok(DBcustomer);
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