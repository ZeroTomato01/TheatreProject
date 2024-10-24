using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface IReservationService
{
    public Task<Reservation?> Get(int id);
    public Task<List<Reservation>> GetBatch(List<int> ids);
    public Task<List<Reservation>> GetAll();
    public Task<bool> Post(Reservation reservation);
    public Task<List<bool>> PostBatch(List<Reservation> reservations);
    public Task<bool> Update(Reservation reservation);
    public Task<List<bool>> UpdateBatch(List<Reservation> reservations);
    public Task<bool> Delete(int id);
    public Task<List<bool>> DeleteBatch(List<int> ids);
    public Task<bool> CheckReservation(int id);
}

