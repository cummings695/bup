using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BestUnitPriceApp.Models;
using Microsoft.Extensions.Logging;

namespace BestUnitPriceApp.Services;

public class RestaurantService : SecuredService, IRestaurantService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;
    
    public RestaurantService(
        HttpClient httpClient,
        ICurrentRestaurantService currentRestaurantService,
        ICurrentUserService currentUserService) : base(currentRestaurantService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }

    public async Task<Restaurant> GetAsync(long id)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, $"api/restaurants/{id}"));

        try
        {
            using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

            var response = _httpClient.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                return await response.Content.ReadFromJsonAsync<Restaurant>(options);
            }
        }
        catch (Exception e)
        {

        }

        return null;
    }

    public async Task<List<Restaurant>> GetAsync()
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, "api/restaurants/mine"));
        
        var user = _currentUserService.Get();
        try
        {
            using var request = this.GetHttpRequestMessage(user, HttpMethod.Get, uri);
            
            var response = _httpClient.SendAsync(request).Result;
            
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync();
                
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                return await response.Content.ReadFromJsonAsync<List<Restaurant>>(options);
            }
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            var text = string.Empty;
        }
        
        return new List<Restaurant>();
    }
}