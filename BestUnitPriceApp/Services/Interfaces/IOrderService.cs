namespace BestUnitPriceApp.Services;

public interface IOrderService
{
    public Task<Order> GetAsync(long id);
}