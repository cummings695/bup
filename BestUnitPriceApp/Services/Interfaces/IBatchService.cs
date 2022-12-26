
namespace BestUnitPriceApp.Services;

public interface IBatchService
{
    public Task<PagedList<Batch>> GetAsync(
        int page, int pageSize, 
        int hydrationLevel = 2, 
        string statusCode = "active");

    public Task<Batch> GetAsync(long id);
}