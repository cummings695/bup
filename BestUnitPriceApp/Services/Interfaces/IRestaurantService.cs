using BestUnitPriceApp.Models;

namespace BestUnitPriceApp.Services;

public interface IRestaurantService
{
    public Task<Restaurant> GetAsync(long id);

    public Task<List<Restaurant>> GetAsync();
}