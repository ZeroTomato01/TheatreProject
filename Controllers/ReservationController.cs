using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Controllers
{
    [Route($"{Globals.Version}/Reservation")]
    public class ReservationController : Controller
    {
        ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetReservation([FromQuery] int id = 0)
        {
            return await _reservationService.GetReservation(id);
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatchReservations([FromQuery] List<int> ids)
        {
            return await _reservationService.GetBatchReservations(ids);
        }

        [HttpPost()]
        protected async Task<IActionResult> PostReservation([FromBody] Reservation reservation)
        {
            return await _reservationService.PostReservation(reservation);
        }

        [HttpPatch()]
        public async Task<IActionResult> UpdateReservation([FromBody] Reservation reservation)
        {
            return await _reservationService.UpdateReservation(reservation);
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteReservation([FromQuery] int id)

        {  
            return await _reservationService.DeleteReservation(id);
        }
    }
}
