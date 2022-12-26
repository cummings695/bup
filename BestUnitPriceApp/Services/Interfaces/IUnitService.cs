using BestUnitPrice.Application.Common.Models;
using BestUnitPriceApp.Models;

namespace BestUnitPriceApp.Services;

public interface IUnitService
{
    public Task<(Result Result, List<Unit> Units)> GetAsync();
}