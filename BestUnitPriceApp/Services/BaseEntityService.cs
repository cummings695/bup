using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BestUnitPriceApp.Services
{
    internal class BaseEntityService<T> : SecuredService
    {
        public async Task<T> GetAsync(long id)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, $"api/{typeof(T).Name}s/{id}"));
            var user = _currentUserService.Get();

            try
            {
                using var request = this.GetHttpRequestMessage(user, HttpMethod.Get, uri);

                var response = _httpClient.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var result = await response.Content.ReadFromJsonAsync<T>(options);
                    return result;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                var text = string.Empty;
            }

            return default;
        }

        public BaseEntityService(
            ICurrentRestaurantService currentRestaurantService,
            ICurrentUserService currentUserService,
            HttpClient httpClient) : base(currentRestaurantService)
        {
            _httpClient = httpClient;

        }

        private readonly HttpClient _httpClient;
        private readonly ICurrentUserService _currentUserService;

    }
}
