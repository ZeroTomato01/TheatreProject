using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatreProject.Models;

public class CustomerService : ICustomerService
{
    
    private DatabaseContext _context;
    // private ICustomerService _customerService;
    // private IReservationService _reservationService;

    public CustomerService(DatabaseContext context)
    {
        _context = context;
        // _customerService = customerService;
        // _reservationService = reservationService;
    }
    public async Task<Customer?> Get(int id)
    {
        var DBCustomer = await _context.Customer.FindAsync(id);
        return DBCustomer;
        
       
        
    }

    public async Task<Customer?> GetByMail(Customer customer)
    {
        var DBCustomer = await _context.Customer
        .Where(c => c.Email == customer.Email)
        .FirstOrDefaultAsync();
        return DBCustomer;
    }
    public async Task<bool> Post(Customer customer)
    {
        if(customer is not null)
        {
            //if (customer.CustomerId == 0) return new BadRequestObjectResult($"id was 0, customer isn't posted");
            var DBcustomer = await _context.Customer.FindAsync(customer.CustomerId);
            if(DBcustomer is not null) return false;
            else
            {
                if(customer.Email is null) return false;
                if(customer.FirstName is null) return false;
                if(customer.LastName is null) return false;

                await _context.Customer.AddAsync(customer);
                _context.SaveChanges();
                return true;
                //return new OkObjectResult($"customer was added to database: {customer}");
            }
        }
        //else return new BadRequestObjectResult("given customer was null");
        return false;
    }
    public async Task<bool> Update(Customer customer)
    {
        var DBcustomer = await _context.Customer.FindAsync(customer.CustomerId);
        if(DBcustomer is not null)
        {
            DBcustomer.Email = customer.Email;
            DBcustomer.FirstName = customer.FirstName;
            DBcustomer.LastName = customer.LastName;
            _context.SaveChanges();

            return true;
        }
        else return false;
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
}