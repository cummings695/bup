using BestUnitPrice.Application.Common.Models;
using System.Net.Http.Json;

namespace BestUnitPriceApp.Services;

public class ZoneService : SecuredService, IZoneService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly HttpClient _httpClient;

    public ZoneService(
        HttpClient httpClient,
        ICurrentUserService currentUserService,
        ICurrentRestaurantService currentRestaurantService) : base(currentRestaurantService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }

    public async Task<(Result Result, List<Zone> Zones)> GetAsync()
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, $"api/zones"));

        try
        {
            using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

            var response = _httpClient.SendAsync(request).Result;

            if (!response.IsSuccessStatusCode)
                return (response.ToApplicationResult(), null);

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = await response.Content.ReadFromJsonAsync<List<Zone>>(options);

            return (response.ToApplicationResult(), result);
        }
        catch (Exception e)
        {
            return (Result.Failure(new[] { e.Message }), null);
        }
    }
}
