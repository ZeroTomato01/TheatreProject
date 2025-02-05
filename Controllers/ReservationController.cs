using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Controllers
{
    [Route($"/Reservation")]
    public class ReservationController : Controller
    {
        IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // public IActionResult ViewReservationPage()
        // {
        //     return View();
        // }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] int? id = 0)
        {
            if (id != null && id != 0)
            {
                var result = await _reservationService.Get((int)id);
                if (result is not null)
                {
                    return Ok(result);
                }
                return NotFound(result);}
            else{
                var result = await _reservationService.GetAll();
             return Ok(result);
            }
        }

        [HttpGet("batch")]
        public async Task<IActionResult> GetBatch([FromQuery] List<int> ids)
        {
            var result = await _reservationService.GetBatch(ids);
            return Ok(result);
        }

        [HttpGet("count")] 
        public async Task<IActionResult> GetCount()
        {
            var result = await _reservationService.GetAll();
            return Ok(result.Count);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] Reservation reservation)
        {
            bool result = await _reservationService.Post(reservation);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("batch")]
        public async Task<IActionResult> PostBatch([FromBody] List<Reservation> reservations)
        {
            var result = await _reservationService.PostBatch(reservations);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully posted");
            }
            return BadRequest();
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] Reservation reservation)
        {
            bool result =  await _reservationService.Update(reservation);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<Reservation> reservations)
        {
            var result = await _reservationService.UpdateBatch(reservations);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully updated");
            }
            return BadRequest();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteReservation([FromQuery] int id)

        {  
            bool result =  await _reservationService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteBatch([FromQuery] List<int> ids)
        {
            var result = await _reservationService.DeleteBatch(ids);
            if (result.Contains(true))
            {
                int trueCount = result.Count(x => x == true);
                return Ok($"{trueCount} out of {result.Count} got succesfully deleted");
            }
            return BadRequest();
        }
    }
}
