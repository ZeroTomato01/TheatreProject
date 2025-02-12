using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

public interface ITheatreShowDateService
{
    public Task<IActionResult> GetAll();
    public Task<IActionResult> GetCount();
    public Task<IActionResult> GetAllFuture();
    public Task<IActionResult> GetTheatreShowDate(int id);
    public Task<IActionResult> PostTheatreShowDate(TheatreShowDate theatreShowDate);
    public Task<IActionResult> UpdateTheatreShowDate(TheatreShowDate theatreShowDate);
    public Task<IActionResult> DeleteTheatreShowDate(int id);
    public Task<bool> CheckTheatreShowDate(int id);
}