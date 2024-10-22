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
        Reservation DBReservation = await _reservationService.GetReservation(id);
        if(DBReservation is not null)
        {
            return new OkObjectResult(DBReservation);
        }
        else return new BadRequestObjectResult($"no threatre with given id: {id} was found in database");
    }
    protected async Task<IActionResult> PostReservation([FromBody] Reservation reservation)
    {
        return await _reservationService.PostReservation(reservation);
    }
    public async Task<IActionResult> UpdateReservation([FromBody] Reservation reservation)
    {
        return await _reservationService.UpdateReservation(reservation);
    }
    public async Task<IActionResult> DeleteReservation([FromQuery] int id)

    {  
        return await _reservationService.DeleteReservation(id);
    }

}