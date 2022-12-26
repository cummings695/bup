namespace BestUnitPriceApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IRestaurantService _restaurantService;
    //private readonly ICurrentRestaurantService _currentRestaurantService;
    private readonly SelectedRestaurantTracker _selectedRestaurantTracker;

    public LoginViewModel(
        ICurrentUserService currentUserService,
        IAuthenticationService authenticationService,
        //ICurrentRestaurantService currentRestaurantService,
        SelectedRestaurantTracker selectedRestaurantTracker,
        IRestaurantService restaurantService)
    {
        _currentUserService = currentUserService;
        _authenticationService = authenticationService;
        _restaurantService = restaurantService;
        //_currentRestaurantService = currentRestaurantService;
        _selectedRestaurantTracker = selectedRestaurantTracker;

        Title = "Login";
    }

    [ObservableProperty]
    private string _email = "cummings695@gmail.com";

    [ObservableProperty]
    private string _password = "cuMMings!403";

    [ObservableProperty]
    private string _errorMessage;

    [RelayCommand]
    async Task ValidateLogin()
    {
        ErrorMessage = string.Empty;
        AuthorizationTicket ticket = null;
        try
        {
            ticket = await _authenticationService.LoginAsync(this.Email, this.Password);
            if (ticket.AccessToken != null)
            {
                // add the ticket to the auth service
                var user = _currentUserService.Set(ticket);
                if (user.SelectedRestaurantId.HasValue)
                    _selectedRestaurantTracker.TrackRestaurant(await _restaurantService.GetAsync(user.SelectedRestaurantId.Value));
                //_currentRestaurantService.Restaurant = rest.Result;
                //user.SelectedRestaurant = _restaurantService.GetAsync(restId);
                //user?.Restaurants?.FirstOrDefault()?.Id;

                await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ErrorMessage = e.Message;
        }
        finally
        {
            if (ticket == null || ticket.AccessToken == null)
            {
                ErrorMessage = "Invalid username / password combination.";
            }
        }
    }
}