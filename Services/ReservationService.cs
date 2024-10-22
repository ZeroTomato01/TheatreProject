using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;
using Microsoft.EntityFrameworkCore;

public class ReservationService : IReservationService
{
    
    private DatabaseContext _context;

    public ReservationService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> Get(int id)
    {
        var result = await _context.Reservation.FindAsync(id);
        return result;
    }

    public async Task<List<Reservation>> GetBatch(List<int> ids)
    {
        var result = await _context.Reservation.
                                    Where(x=>ids.Contains(x.ReservationId)).
                                    ToListAsync();
        return result;
    }

    public async Task<List<Reservation>> GetAll()
    {
        var result = await _context.Reservation.ToListAsync();
        return result;
    }

    public async Task<bool> Post(Reservation reservation)
    {
        if(reservation is not null)
        {
            var register = await _context.Reservation.FindAsync(reservation.ReservationId);

            if(register is not null)
            {
                return false;
            }
            else
            {
                await _context.Reservation.AddAsync(reservation);
                _context.SaveChanges();
                return true;
            }
        }
        return false;
    }
    public async Task<List<bool>> PostBatch(List<Reservation> reservations)
    {
        var results = new List<bool> {};
        foreach (Reservation reservation in reservations)
        {
            bool result = await Post(reservation);
            results.Add(result);
        }
        return results;
    }
    public async Task<bool> Update(Reservation reservation)
    {
        var register = await _context.Reservation.FindAsync(reservation.ReservationId);
        if(register is not null)
        {
            register.AmountOfTickets = reservation.AmountOfTickets;
            register.Customer = reservation.Customer;
            register.TheatreShowDate = reservation.TheatreShowDate;
            register.Used = reservation.Used;
    
            //DBShow = theatreShow;
            _context.SaveChanges();

            return true;
        }
        return false;
    }

    public async Task<List<bool>> UpdateBatch(List<Reservation> reservations)
    {
        var results = new List<bool> {};
        foreach (Reservation reservation in reservations)
        {
            bool result = await Update(reservation);
            results.Add(result);
        }
        return results;
    }

    public async Task<bool> Delete(int id)
    {
        var register = await _context.Reservation.FindAsync(id);

        if(register is not null)
        {
            _context.Remove(register);
            _context.SaveChanges();
            return true;
        }
        return false;
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