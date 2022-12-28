using BestUnitPriceApp.Views.Popups;

namespace BestUnitPriceApp.ViewModels;

public partial class LoadingViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IConnectivity _connectivity;

    private readonly ICurrentUserService _currentUserService;
    private readonly ICurrentRestaurantService _currentRestaurantService;
    private readonly IRestaurantService _restaurantService;
    private readonly SelectedRestaurantTracker _selectedRestaurantTracker;

    [ObservableProperty] private bool _isLoading = true;

    public LoadingViewModel(
        ICurrentUserService currentUserService,
        IAuthenticationService authenticationService,
        IConnectivity connectivity,
        IGeolocation geolocation,
        SelectedRestaurantTracker selectedRestaurantTracker,
        ICurrentRestaurantService currentRestaurantService,
        IRestaurantService restaurantService)
    {
        this.Title = "Loading";
        _currentUserService = currentUserService;
        _connectivity = connectivity;
        _restaurantService = restaurantService;
        _authenticationService = authenticationService;
        _currentRestaurantService = currentRestaurantService;
        _selectedRestaurantTracker = selectedRestaurantTracker;

        CheckUserAuthentication();
    }
    
        private async void CheckUserAuthentication()
    {
        if (await CheckAuthentication())
        {
            var user = _currentUserService.Get();
            // load the users restaurants
            //if (_currentRestaurantService.Restaurant != null)
            //    await Shell.Current.GoToAsync($"//{nameof(RestaurantPage)}");

            // does the user have a selected Restaurant assigned.
            if (_currentUserService.Get().SelectedRestaurantId == null)
                _currentUserService.Get().SelectedRestaurantId = 3;
                //await Shell.Current.GoToAsync($"//{nameof(RestaurantPage)}");

            var restaurant = await _restaurantService.GetAsync(_currentUserService.Get().SelectedRestaurantId.Value);

            if (restaurant == null)
                throw new ApplicationException("ASDFADS ASDFSDF ASDF ADSF F");
                //await Shell.Current.GoToAsync($"//{nameof(RestaurantPage)}");

            _selectedRestaurantTracker.TrackRestaurant(restaurant);

            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
        else
        {
            
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }

    private async Task<bool> CheckAuthentication()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!",
                $"Please check internet and try again.", "OK");
            return false;
        }

        //_currentUserService.Clear();

        // determine if we are logged in.
        var user = _currentUserService.Get();
        if (user == null)
        {
            return false;
        }

        // check that the refresh token works

        var response = await _authenticationService.RefreshAsync(user.Token, user.RefreshToken);

        return response.Match(
            b => { return true; },
            e => { return false; });
    }

    public void DisplayRestaurantPicker(View views)
    {
        var view = (VisualElement)Activator.CreateInstance(typeof(RestaurantSelectionPopup));
        /*
        views.Navigation.ShowPopupAsync();
        if (view is Popup<string?> popup)
        {
            
            var result = await Navigation.ShowPopupAsync(popup);
            await Application.Current.MainPage.DisplayAlert("Popup Result", result, "OKAY");
        }
        else if (view is BasePopup basePopup)
        {
            Navigation.ShowPopup(basePopup);
        }
        else if (view is Page page)
        {
            await Navigation.PushAsync(page);
        }
        */
    }
}
