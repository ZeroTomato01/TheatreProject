using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface ICustomerService
{
    public Task<Customer?> Get(int id);
    public Task<List<Customer>> GetBatch(List<int> ids);
    public Task<List<Customer>> GetAll();
    public Task<bool> Post(Customer customer);
    public Task<List<bool>> PostBatch(List<Customer> customers);
    public Task<bool> Update(Customer Customer);
    public Task<List<bool>> UpdateBatch(List<Customer> customers);
    public Task<bool> Delete(int id);
    public Task<List<bool>> DeleteBatch(List<int> ids);
}