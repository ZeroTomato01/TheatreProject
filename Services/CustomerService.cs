using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

[Route("Customer")]
public class CustomerService : ICustomerService
{
    
    private DatabaseContext _context;
    private IReservationService _reservationService;

    public CustomerService(DatabaseContext context, IReservationService reservationService)
    {
        _context = context;
        _reservationService = reservationService;
    }
    public async Task<IActionResult> GetCustomer(int id)
    {
        var DBCustomer = await _context.Customer.FindAsync(id);
        if(DBCustomer is not null)
        {
            return new OkObjectResult(DBCustomer);
        }
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
        
    }
    [HttpPost()]
    public async Task<bool> PostCustomer(Customer customer)
    {
        // if(customer is not null)
        // {
        //     //if (customer.CustomerId == 0) return new BadRequestObjectResult($"id was 0, customer isn't posted");
        //     var DBCustomer = await _context.Customer.FindAsync(customer.CustomerId);
        //     if(DBCustomer is not null)
        //     {
        //         //return new BadRequestObjectResult($"there's already a customer with id: {customer.CustomerId}in databse, use update instead");
        //     }
        //     else
        //     {
        //         // if(DBCustomer.Email is null) return new BadRequestObjectResult($"email was not given, customer isn't posted");
        //         // if(DBCustomer.FirstName is null) return new BadRequestObjectResult($"first name was not given, customer isn't posted");
        //         // if(DBCustomer.LastName is null) return new BadRequestObjectResult($"last name was not given, customer isn't posted");
        //         if(DBCustomer.Reservations is null) return new BadRequestObjectResult($"reservations were not given, customer isn't posted");
            
        //         else 
        //         {
        //             // foreach(Reservation reservation in DBCustomer.Reservations)
        //             // {
        //             //     bool success = await _reservationService.CheckReservation(reservation.ReservationId);
        //             //     if(success is false) return new BadRequestObjectResult($"reservation with id: {reservation.ReservationId} wasn't found in database, customer isn't posted");
        //             // }
        //             foreach(Reservation reservation in DBCustomer.Reservations)
        //             {
        //                 if (reservation.Customer is null) return false;
        //                 if (reservation.)

        //             }

        //         }

                
        //         return true;

        //         await _context.Customer.AddAsync(customer);
        //         _context.SaveChanges();
        //         //return new OkObjectResult($"customer was added to database: {customer}");
        //     }
        // }
        // //else return new BadRequestObjectResult("given customer was null");
        return false;
    }
    public async Task<IActionResult> UpdateCustomer(Customer customer)
    {
        var DBCustomer = await _context.Customer.FindAsync(customer.CustomerId);
        if(DBCustomer is not null)
        {
            DBCustomer.Email = customer.Email;
            DBCustomer.FirstName = customer.FirstName;
            DBCustomer.LastName = customer.LastName;
            DBCustomer.Reservations = customer.Reservations;
            _context.SaveChanges();

            return new OkObjectResult($"Customer updated to {customer}");
        }
        else return new BadRequestObjectResult($"no Customer with given id: {customer.CustomerId} was found in database");
    }
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var DBCustomer = await _context.Customer.FindAsync(id);
        if(DBCustomer is not null)
        {
            _context.Remove(DBCustomer);
            _context.SaveChanges();
            return new OkObjectResult("customer deleted");
        }
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
        
    }
}