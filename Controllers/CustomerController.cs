using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public class CustomerController : Controller
{
    CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> GetCustomer([FromQuery] int id)
    {
        return await _customerService.GetCustomer(id);
    }
    protected async Task<IActionResult> PostCustomer([FromBody] Customer customer)
    {
        return await _customerService.PostCustomer(customer);
    }
    public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
    {
        return await _customerService.UpdateCustomer(customer);
    }
    public async Task<IActionResult> DeleteCustomer([FromQuery] int id)
    {  
        return await _customerService.DeleteCustomer(id);
    }
}