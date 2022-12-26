using System.Net.Http.Headers;
using BestUnitPrice.Application.Common.Exceptions;
using BestUnitPriceApp.Models;

namespace BestUnitPriceApp.Services;

public class SecuredService
{
    protected readonly ICurrentRestaurantService _currentRestaurantService;
    
    public SecuredService(ICurrentRestaurantService currentRestaurantService)
    {
        _currentRestaurantService = currentRestaurantService;
    }
    
    public HttpRequestMessage GetHttpRequestMessage(ApplicationUser user, HttpMethod method, Uri uri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user?.Token );

        if (_currentRestaurantService.CurrentRestaurant != null)
        request.Headers.Add("RestaurantId", _currentRestaurantService.CurrentRestaurant?.Id.ToString());
        
        return request;
    }
}