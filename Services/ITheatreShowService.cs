using Microsoft.AspNetCore.Mvc;
using TheatreProject.Models;

namespace TheatreProject.Services;


public interface ITheatreShowService {
    public Task<TheatreShow?> Get(int id);
    public Task<List<TheatreShow>> GetBatch(List<int> ids);
    public Task<List<TheatreShow>> GetAll();
    public Task<bool> Post(TheatreShow theatreShow);
    public Task<List<bool>> PostBatch(List<TheatreShow> theatreShowD);
    public Task<bool> Update(TheatreShow theatreShow);
    public Task<List<bool>> UpdateBatch(List<TheatreShow> theatreShowD);
    public Task<bool> Delete(int id);
    public Task<List<bool>> DeleteBatch(List<int> ids);
}