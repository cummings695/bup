using System.Net.Http.Json;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public partial class UnitService : SecuredService, IUnitService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly HttpClient _httpClient;

    public UnitService(
        HttpClient httpClient,
        ICurrentUserService currentUserService,
        ICurrentRestaurantService currentRestaurantService) : base(currentRestaurantService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }


    public async Task<Result<PagedList<Unit>>> GetAsync(int? page, int? pageSize)
    {
        var url = string.Format(Constants.RestUrl, "api/units?");
        if (page.HasValue && pageSize.HasValue)
        {
            url += $"page={page}&pageSize={pageSize}";
        }

        Uri uri = new Uri(url);
        using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

        var response = _httpClient.SendAsync(request).Result;

        if (!response.IsSuccessStatusCode)
            return new Result<PagedList<Unit>>(new HttpRequestException(response.ReasonPhrase));

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return await response.Content.ReadFromJsonAsync<PagedList<Unit>>(options);
    }
}