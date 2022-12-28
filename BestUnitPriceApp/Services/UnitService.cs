using System.Net.Http.Json;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public partial class UnitService : SecuredService, IUnitService
{
    private readonly ICurrentRestaurantService _currentRestaurantService;
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


    public async Task<Result<List<Unit>>> GetAsync()
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, $"api/units"));

        using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

        var response = _httpClient.SendAsync(request).Result;

        if (!response.IsSuccessStatusCode)
            return new Result<List<Unit>>(new HttpRequestException(response.ReasonPhrase));

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = await response.Content.ReadFromJsonAsync<PagedList<Unit>>(options);

        return result.Items;
    }
}