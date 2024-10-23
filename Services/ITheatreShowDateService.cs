using TheatreProject.Models;

public interface ITheatreShowDateService
{
    public Task<TheatreShowDate?> Get(int id);
    public Task<List<TheatreShowDate>> GetBatch(List<int> ids);
    public Task<List<TheatreShowDate>> GetAll();
    public Task<bool> Post(TheatreShowDate theatreShowDate);
    public Task<List<bool>> PostBatch(List<TheatreShowDate> theatreShowDates);
    public Task<bool> Update(TheatreShowDate theatreShowDate);
    public Task<List<bool>> UpdateBatch(List<TheatreShowDate> theatreShowDates);
    public Task<bool> Delete(int id);
    public Task<List<bool>> DeleteBatch(List<int> ids);
}