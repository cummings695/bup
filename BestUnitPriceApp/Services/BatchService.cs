using System.Net.Http.Json;

namespace BestUnitPriceApp.Services;

public class BatchService : SecuredService, IBatchService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;

    public BatchService(
        HttpClient httpClient,
        ICurrentRestaurantService currentRestaurantService,
        ICurrentUserService currentUserService) : base(currentRestaurantService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }

    public async Task<PagedList<Batch>> GetAsync(int page, int pageSize, int hydrationLevel = 2,
        string statusCode = "active")
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl,
            $"api/batches?page={page}&pagesize={pageSize}" +
            $"&hydrationlevel={hydrationLevel}&statusCode={statusCode}" +
            $"&sort=-number&"));
        var user = _currentUserService.Get();
        try
        {
            using var request = this.GetHttpRequestMessage(user, HttpMethod.Get, uri);

            var response = _httpClient.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                var result = await response.Content.ReadFromJsonAsync<PagedList<Batch>>(options);
                return result;
            }
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            var text = string.Empty;
        }

        return new PagedList<Batch>();
    }
    
    public async Task<Batch> GetAsync(long id)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, $"api/batches/{id}"));
        var user = _currentUserService.Get();
        try
        {
            using var request = this.GetHttpRequestMessage(user, HttpMethod.Get, uri);

            var response = _httpClient.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var strResult = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                var result = await response.Content.ReadFromJsonAsync<Batch>(options);
                return result;
            }
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            var text = string.Empty;
        }

        return new Batch();
    }
}