using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface ICustomerService
{
    public Task<IActionResult> Get(int id);
    public Task<bool> Post(Customer customer);
    public Task<IActionResult> Update(Customer customer);
    public Task<IActionResult> Delete(int id);
}