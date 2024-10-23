using TheatreProject.Models;

public interface IVenueService
{
    public Task<Venue?> Get(int id);
    public Task<List<Venue>> GetBatch(List<int> ids);
    public Task<List<Venue>> GetAll();
    public Task<bool> Post(Venue venue);
    public Task<List<bool>> PostBatch(List<Venue> venues);
    public Task<bool> Update(Venue venue);
    public Task<List<bool>> UpdateBatch(List<Venue> venues);
    public Task<bool> Delete(int id);
    public Task<List<bool>> DeleteBatch(List<int> ids);
}