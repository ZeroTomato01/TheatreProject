using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface IReservationService
{
    public Task<IActionResult> GetReservation(int id);
    public Task<IActionResult> PostReservation(Reservation reservation);
    public Task<IActionResult> UpdateReservation(Reservation reservation);
    public Task<IActionResult> DeleteReservation(int id);
}