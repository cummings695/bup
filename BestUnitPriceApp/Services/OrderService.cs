using System.Net.Http.Json;

namespace BestUnitPriceApp.Services;

public class OrderService : SecuredService, IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;

    public OrderService(
        HttpClient httpClient,
        ICurrentRestaurantService currentRestaurantService,
        ICurrentUserService currentUserService) : base(currentRestaurantService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }

    public async Task<Order> GetAsync(long id)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl,
            $"api/orders/{id}"));

        try
        {
            using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

            var response = _httpClient.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var result = await response.Content.ReadFromJsonAsync<Order>(options);
                return result;
            }
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            var text = string.Empty;
        }

        return null;
    }
}