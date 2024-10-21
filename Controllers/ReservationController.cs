using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public class ReservationController : Controller
{
    ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    public async Task<IActionResult> GetReservation([FromQuery] int id)
    {
        return await _reservationService.GetReservation(id);
    }
    protected async Task<IActionResult> PostTheatreShow([FromBody] Reservation reservation)
    {
        return await _reservationService.PostReservation(reservation);
    }
    public async Task<IActionResult> UpdateTheatreShow([FromBody] Reservation reservation)
    {
        return await _reservationService.UpdateReservation(reservation);
    }
    public async Task<IActionResult> DeleteTheatreShow([FromQuery] int id)
    {  
        return await _reservationService.DeleteReservation(id);
    }

}