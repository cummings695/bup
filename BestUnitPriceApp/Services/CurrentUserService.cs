using System.IdentityModel.Tokens.Jwt;

namespace BestUnitPriceApp.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ICurrentRestaurantService _currentRestaurantService;
    //private readonly IRestaurantService _restaurantService;

    public CurrentUserService (
        //IRestaurantService restaurantService,
        ICurrentRestaurantService currentRestaurantService)
    {
        _currentRestaurantService = currentRestaurantService;
        //_restaurantService = restaurantService;
    }

    public ApplicationUser Set(string token, string refresh)
    {
        Preferences.Set(UserTokenKey, token);
        Preferences.Set(RefreshTokenKey, refresh);
        _currentUser = DecodeToken(token);
        _currentUser.RefreshToken = refresh;
        return _currentUser;
    }
    
    public ApplicationUser Set(AuthorizationTicket ticket)
    {
        return Set(ticket.AccessToken, ticket.RefreshToken);
    }

    public ApplicationUser Get()
    {
        if (_currentUser != null) return _currentUser;
        
        var token = Preferences.Get(UserTokenKey, null);
        if (!string.IsNullOrEmpty(token))
        {
            // decode the string to 
            _currentUser = DecodeToken(token);
            _currentUser.RefreshToken = Preferences.Get(RefreshTokenKey, null);
        }
        return _currentUser;
    }

    public void Clear()
    {
        Preferences.Clear(UserTokenKey);
        Preferences.Clear(RefreshTokenKey);
        Preferences.Clear();
        _currentUser = null;
    }
    
    private const string UserTokenKey = "Token";
    private const string RefreshTokenKey = "RefreshToken";
    private const string SelectedRestaurantKey = "SelectedRestaurantId";
    
    private ApplicationUser _currentUser = null;
    
    private ApplicationUser DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.ReadToken(token) as JwtSecurityToken;

        var restid = securityToken.Claims.FirstOrDefault(c => c.Type == "selectedRestaurant");
        long.TryParse(restid.Value, out long serverRestaurantId);
        long.TryParse(Preferences.Get(SelectedRestaurantKey, null), out var restaurantId);
        var user = new ApplicationUser
        {
            UserId = securityToken.Id,
            Token = token,
            SelectedRestaurantId = restaurantId != 0
                ? restaurantId
                : serverRestaurantId != 0
                    ? serverRestaurantId
                    : null
        };

        return user;
    }

    public void Store()
    {
        // save local settings
        if (_currentUser == null) return;
        
        //if(_currentUser.SelectedRestaurant.HasValue)
        //    Preferences.Set(SelectedRestaurantKey, _currentUser.SelectedRestaurant.Value.ToString());

        return;
    }
}