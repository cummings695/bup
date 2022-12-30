using BestUnitPriceApp.Models;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public interface IUnitService
{
    public Task<Result<PagedList<Unit>>> GetAsync(int? page, int? pageSize);
}