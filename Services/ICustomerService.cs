using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface ICustomerService
{
    public Task<Customer> Get(int id);
    public Task<Customer> GetByMail(string email);
    public Task<bool> Post(Customer customer);
    public Task<bool> Update(Customer customer);
    public Task<bool> Delete(int id);
}