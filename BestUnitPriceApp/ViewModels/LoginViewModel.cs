namespace BestUnitPriceApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    readonly ICurrentUserService _currentUserService;
    readonly IAuthenticationService _authenticationService;
    readonly IRestaurantService _restaurantService;
    readonly IConnectivity _connectivity;
    readonly SelectedRestaurantTracker _selectedRestaurantTracker;

    public LoginViewModel(
        ICurrentUserService currentUserService,
        IAuthenticationService authenticationService,
        IConnectivity connectivity,
        SelectedRestaurantTracker selectedRestaurantTracker,
        IRestaurantService restaurantService)
    {
        _currentUserService = currentUserService;
        _authenticationService = authenticationService;
        _restaurantService = restaurantService;
        _selectedRestaurantTracker = selectedRestaurantTracker;
        _connectivity = connectivity;
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
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
            return;
        }
        
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