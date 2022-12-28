namespace BestUnitPriceApp.Services;

public interface IInventoryItemService
{
    public Task<InventoryItem> GetAsync(long id); 
    
    public Task<PagedList<InventoryItem>> GetByZoneAsync(long zoneId, int? page = null, int? pageSize = null);
}