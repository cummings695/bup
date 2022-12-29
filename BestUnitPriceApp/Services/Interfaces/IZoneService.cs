using BestUnitPriceApp.Models;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public interface IZoneService
{
    public Task<Result<List<Zone>>> GetAsync();
}