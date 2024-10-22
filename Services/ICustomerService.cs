using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface ICustomerService
{
    public Task<IActionResult> GetCustomer(int id);
    public Task<bool> PostCustomer(Customer customer);
    public Task<IActionResult> UpdateCustomer(Customer customer);
    public Task<IActionResult> DeleteCustomer(int id);
}