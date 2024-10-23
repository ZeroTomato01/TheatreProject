using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace TheatreProject.Services;

public class CustomerService : ICustomerService
{
    private DatabaseContext _context;

    public CustomerService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Customer?> Get(int id)
    {
        var register = await _context.Customer.FindAsync(id);
        return register; 
    }

    public async Task<List<Customer>> GetBatch(List<int> ids)
    {
        var result = await _context.Customer.
                                    Where(x=>ids.Contains(x.CustomerId)).
                                    ToListAsync();
        return result;
    }

    public async Task<List<Customer>> GetAll(string? firstName = null, string? lastName = null, string? email = null)
    {
        var query = _context.Customer.AsQueryable();
        
        if (firstName != null)
        {
            query = query.Where(x => x.FirstName == firstName);
        }
        if (lastName != null)
        {
            query = query.Where(x => x.LastName == lastName);
        }
        if (email != null)
        {
            query = query.Where(x => x.Email == email);
        }

        // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<bool> Post(Customer customer)
    {
        if(customer is not null)
        {
            var register = await _context.Customer.FindAsync(customer.CustomerId);

            if(register is not null)
            {
                return false;
            }
            else
            {
                await _context.Customer.AddAsync(customer);
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public async Task<List<bool>> PostBatch(List<Customer> customers)
    {
        var results = new List<bool> {};
        foreach (Customer customer in customers)
        {
            bool result = await Post(customer);
            results.Add(result);
        }
        return results;
    }

    public async Task<bool> Update(Customer customer)
    {
        var DBcustomer = await _context.Customer.FindAsync(customer.CustomerId);
        if(DBcustomer is not null)
        {
            DBcustomer.Email = customer.Email;
            DBcustomer.FirstName = customer.FirstName;
            DBcustomer.LastName = customer.LastName;
            DBcustomer.Reservations = customer.Reservations;
            _context.SaveChanges();

            return true;
        }
        else return false;
    }

    public async Task<List<bool>> UpdateBatch(List<Customer> customers)
    {
        var results = new List<bool> {};
        foreach (Customer customer in customers)
        {
            bool result = await Update(customer);
            results.Add(result);
        }

        return results;
    }

    public async Task<bool> Delete(int id)
    {
        var DBcustomer = await _context.Customer.FindAsync(id);
        if(DBcustomer is not null)
        {
            _context.Remove(DBcustomer);
            _context.SaveChanges();
            return true;
        }
        else return false;
    }

    public async Task<List<bool>> DeleteBatch(List<int> ids)
    {
        var results = new List<bool> {};
        foreach (int id in ids)
        {
            bool result = await Delete(id);
            results.Add(result);
        }
        return results;
    }
}