namespace BestUnitPriceApp.Services;

public interface IInventoryItemService
{
    public Task<InventoryItem> GetAsync(long id); 
    
    public Task<List<InventoryItem>> GetByZoneAsync(long zoneId);

}