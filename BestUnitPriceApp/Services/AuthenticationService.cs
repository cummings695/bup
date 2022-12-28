using BestUnitPriceApp.Common;
using System.Net.Http.Json;
using System.Text;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly HttpClient _httpClient;

    public AuthenticationService(
        HttpClient httpClient,
        ICurrentUserService currentUserService)
    {
        _httpClient = httpClient;
        _currentUserService = currentUserService;
    }


    public AuthorizationTicket Login(string email, string password)
    {
        return LoginAsync(email, password).Result;
    }

    public async Task<AuthorizationTicket> LoginAsync(string email, string password)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, "api/token"));

        var credentials = new { Username = email, Password = password };

        var json = JsonSerializer.Serialize(credentials);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, data);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthorizationTicket>();
        }

        return new AuthorizationTicket();
        //throw new ApplicationException(response.ReasonPhrase);
    }

    public async Task<Result<AuthorizationTicket>> RefreshAsync(string accessToken, string refreshToken)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, "api/token/refresh"));

        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = new StringContent(
            JsonSerializer.Serialize(
                new { AccessToken = accessToken, RefreshToken = refreshToken }), Encoding.UTF8, "application/json");
        
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return new Result<AuthorizationTicket>(new HttpRequestException(response.ReasonPhrase));
            }

            return await response.Content.ReadFromJsonAsync<AuthorizationTicket>();
    }

    public async Task<string> GetAuthToken()
    {
        //if (_currentUserService.Get() == null)
        //{
        //    // redirect to login
        //    await Shell.Current.GoToAsync(nameof(LoginPage), true);
        //}

        return null;
    }
}