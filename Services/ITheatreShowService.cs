using Microsoft.AspNetCore.Components.Web;
using TheatreProject.Models;

namespace TheatreProject.Services;

public interface ITheatreShowService
{
    public Task<TheatreShow?> Get(int id);
    public Task<List<TheatreShow>> GetBatch(List<int> ids);
    public Task<List<TheatreShow>> GetAll(string? title = null, double? maxPrice = null, int? venueId = null); // filters can be added here
    public Task<bool> Post(TheatreShow theatreShow);
    public Task<List<bool>> PostBatch(List<TheatreShow> theatreShowD);
    public Task<bool> Update(TheatreShow theatreShow);
    public Task<List<bool>> UpdateBatch(List<TheatreShow> theatreShowD);
    public Task<bool> Delete(int id);
    public Task<List<bool>> DeleteBatch(List<int> ids);
}