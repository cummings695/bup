using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public interface IVendorService
{
    public Task<Result<PagedList<Vendor>>> GetAsync(int? page, int? pageSize);
}