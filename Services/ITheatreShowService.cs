using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Services;


public interface ITheatreShowService {
    public Task<IActionResult> GetAll();
    public Task<IActionResult> GetTheatreShows(int? id,
            string? title,
            string? description,
            string? location,
            DateTime? startDate,
            DateTime? endDate,
            string? sortBy = "Title",
            bool descending = false);
    protected Task<IActionResult> PostTheatreShow(TheatreShow theatreShow);
    protected Task<IActionResult> UpdateTheatreShow(TheatreShow theatreShow);
    protected Task<IActionResult> DeleteTheatreShow(int id);
    public Task<bool> CheckTheatreShow(int id);
}