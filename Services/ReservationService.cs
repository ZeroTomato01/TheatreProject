using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public class ReservationService : IReservationService
{
    
    private DatabaseContext _context;
    private TheatreShowDateService _theatreShowDateService;

    public ReservationService(DatabaseContext context, TheatreShowDateService theatreShowDateService)
    {
        _context = context;
        _theatreShowDateService = theatreShowDateService;
    }

    public async Task<Reservation?> GetReservation(int id)
    {
        var DBReservation = await _context.Reservation.FindAsync(id);
        return DBReservation;
    }

    public async Task<IActionResult> PostReservation(Reservation reservation)
    {
        if(reservation is not null)
        {
            var DBReservation = await _context.Reservation.FindAsync(reservation.ReservationId);

            if(DBReservation is not null)
            {
                return new BadRequestObjectResult($"there's already a reservation with id: {reservation.ReservationId}in databse, use update instead");
            }
            else
            {
                await _context.Reservation.AddAsync(reservation);
                _context.SaveChanges();
                return new OkObjectResult($"reservation was added to database: {reservation}");
            }
        }
        else return new BadRequestObjectResult("given reservation was null");
    }
    public async Task<IActionResult> UpdateReservation(Reservation reservation)
    {
        var DBReservation = await _context.Reservation.FindAsync(reservation.ReservationId);
        if(DBReservation is not null)
        {
            DBReservation.AmountOfTickets = reservation.AmountOfTickets;
            DBReservation.Customer = reservation.Customer;
            DBReservation.TheatreShowDate = reservation.TheatreShowDate;
            DBReservation.Used = reservation.Used;
    
            //DBShow = theatreShow;
            _context.SaveChanges();

            return new OkObjectResult($"Reservation updated to {reservation}");
        }
        else return new BadRequestObjectResult($"no reservation with given id: {reservation.ReservationId} was found in database");
    }
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var DBReservation = await _context.Reservation.FindAsync(id);

        if(DBReservation is not null)
        {
            _context.Remove(DBReservation);
            _context.SaveChanges();
            return new OkObjectResult("reservation deleted");
        }
        else return new BadRequestObjectResult($"no reservation with given id: {id} was found in database");
        
    }

    public async Task<bool> CheckReservation(int id)
    {
        var DBReservation = await _context.Reservation.FindAsync(id);
        if(DBReservation is not null)
        {
            if(DBReservation.TheatreShowDate is null) return false;
            if(DBReservation.TheatreShowDate.TheatreShowDateId == 0) return false;
            return await _theatreShowDateService.CheckTheatreShowDate(DBReservation.TheatreShowDate.TheatreShowDateId);
        }
        else return false;
    }
}