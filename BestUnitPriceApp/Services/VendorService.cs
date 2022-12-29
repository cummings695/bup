using System.Net.Http.Json;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public partial class VendorService : SecuredService, IVendorService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly HttpClient _httpClient;

    public VendorService(
        HttpClient httpClient,
        ICurrentUserService currentUserService,
        ICurrentRestaurantService currentRestaurantService) : base(currentRestaurantService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }


    public async Task<Result<PagedList<Vendor>>> GetAsync(int? page, int? pageSize)
    {
        var url = string.Format(Constants.RestUrl, $"api/vendors?");
        if (page.HasValue && pageSize.HasValue)
        {
            url += $"page={page}&pagesize={pageSize}";
        }
        Uri uri = new Uri(url);

        using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

        var response = _httpClient.SendAsync(request).Result;

        if (!response.IsSuccessStatusCode)
            return new Result<PagedList<Vendor>>(new HttpRequestException(response.ReasonPhrase));

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = await response.Content.ReadFromJsonAsync<PagedList<Vendor>>(options);

        return result;
    }
}