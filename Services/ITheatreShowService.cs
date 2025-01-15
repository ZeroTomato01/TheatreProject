using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Services;


public interface ITheatreShowService {
    public Task<TheatreShow> GetShow(int id);
    public Task<IActionResult> GetTheatreShows(int? id,
            string? title,
            string? description,
            string? location,
            DateTime? startDate,
            DateTime? endDate,
            string? sortBy = "Title",
            bool descending = false);
    public Task<IActionResult> PostTheatreShow(TheatreShow theatreShow);
    public Task<IActionResult> UpdateTheatreShow(TheatreShow theatreShow);
    public Task<IActionResult> DeleteTheatreShow(int id);
    public Task<bool> CheckTheatreShow(int id);
}