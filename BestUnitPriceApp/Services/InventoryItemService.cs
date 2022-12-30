using BestUnitPriceApp.Models;
using System.Net.Http.Json;

namespace BestUnitPriceApp.Services;

internal class InventoryItemService : BaseEntityService<InventoryItem>, IInventoryItemService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;

    public InventoryItemService(
        HttpClient httpClient,
        ICurrentRestaurantService currentRestaurantService,
        ICurrentUserService currentUserService) : base(currentRestaurantService, currentUserService, httpClient)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }

    public async Task<PagedList<InventoryItem>> GetByZoneAsync(long zoneId, int? page, int? pageSize, bool? active = null)
    {
        var url = $"api/inventoryitems?zoneid={zoneId}&hydrationlevel=3";
        if (page.HasValue && pageSize.HasValue)
        {
            url += $"&page={page}&pagesize={pageSize}";
        }

        if (active.HasValue)
        {
            url += $"&active={active}";
        }
        
        Uri uri = new Uri(string.Format(Constants.RestUrl, url));
        try
        {
            using var request = this.GetHttpRequestMessage(_currentUserService.Get(), HttpMethod.Get, uri);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                var result = await response.Content.ReadFromJsonAsync<PagedList<InventoryItem>>(options);
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

    //public async Task<InventoryItem> GetAsync(long id)
    //{
    //    Uri uri = new Uri(string.Format(Constants.RestUrl, $"api/inventoryitems/{id}"));
    //    var user = _currentUserService.Get();

    //    try
    //    {
    //        using var request = this.GetHttpRequestMessage(user, HttpMethod.Get, uri);

    //        var response = _httpClient.SendAsync(request).Result;

    //        if (response.IsSuccessStatusCode)
    //        {
    //            var content = await response.Content.ReadAsStringAsync();
    //            var options = new JsonSerializerOptions
    //            {
    //                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    //            };

    //            var result = await response.Content.ReadFromJsonAsync<InventoryItem>(options);
    //            return result;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        //Console.WriteLine(e);
    //        var text = string.Empty;
    //    }

    //    return null;
    //}
}