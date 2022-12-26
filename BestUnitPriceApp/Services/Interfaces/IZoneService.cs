using BestUnitPrice.Application.Common.Models;
using BestUnitPriceApp.Models;

namespace BestUnitPriceApp.Services;

public interface IZoneService
{
    public Task<(Result Result, List<Zone> Zones)> GetAsync();
}
