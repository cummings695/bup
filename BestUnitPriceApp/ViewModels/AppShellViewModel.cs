using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestUnitPriceApp.ViewModels;

public partial class AppShellViewModel : ObservableObject
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IConnectivity _connectivity;

    private readonly ICurrentUserService _currentUserService;
    private readonly ICurrentRestaurantService _currentRestaurantService;
    private readonly IRestaurantService _restaurantService;

    [ObservableProperty] private bool _itemsReleased;
    [ObservableProperty] private bool _orderGuideReleased;
    [ObservableProperty] private bool _orderHistoryReleased;
    [ObservableProperty] private bool _restaurantsReleased;
    [ObservableProperty] private bool _systemReleased;

    [ObservableProperty] private bool _systemVendorsReleased;
    [ObservableProperty] private bool _systemUnitsReleased;
    [ObservableProperty] private bool _systemZonesReleased;


    public AppShellViewModel(
        //ICurrentUserService currentUserService,
        //IAuthenticationService authenticationService,
        //IConnectivity connectivity,
        //SelectedRestaurantTracker selectedRestaurantTracker,
        //ICurrentRestaurantService currentRestaurantService,
        //IRestaurantService restaurantService
        )
    {
        //_currentUserService = currentUserService;
        //_connectivity = connectivity;
        //_restaurantService = restaurantService;
        //_authenticationService = authenticationService;
        //_currentRestaurantService = currentRestaurantService;
        //_selectedRestaurantTracker = selectedRestaurantTracker;

        ItemsReleased = true;
        OrderGuideReleased = false;
        OrderHistoryReleased = true;
        RestaurantsReleased = true;
        SystemReleased = true;
        SystemVendorsReleased = true;
        SystemUnitsReleased = false;
        SystemZonesReleased = true;
    }

    [RelayCommand]
    async void SignOut()
    {
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    //public async Task CheckUserAuthentication()
    //{
    //    if (await CheckAuthentication())
    //    {
    //        var user = _currentUserService.Get();
    //        // load the users restaurants
    //        if (_currentRestaurantService.CurrentRestaurant == null)
    //        {
    //            await Shell.Current.DisplayAlert("No Restaurant!",
    //                $"Select a restaurant to continue.", "OK");
    //            //await Shell.Current.GoToAsync($"//{nameof(RestaurantsPage)}");
    //            return;
    //        }


    //        // does the user have a selected Restaurant assigned.
    //        if (_currentUserService.Get().SelectedRestaurantId == null)
    //            _currentUserService.Get().SelectedRestaurantId = 3;
    //        //await Shell.Current.GoToAsync($"//{nameof(RestaurantPage)}");

    //        var restaurant = await _restaurantService.GetAsync(_currentUserService.Get().SelectedRestaurantId.Value);

    //        if (restaurant == null)
    //            throw new ApplicationException("ASDFADS ASDFSDF ASDF ADSF F");
    //        //await Shell.Current.GoToAsync($"//{nameof(RestaurantPage)}");

    //        _selectedRestaurantTracker.TrackRestaurant(restaurant);

    //        await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
    //    }
    //    else
    //    {

    //        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    //    }
    //}

    //private async Task<bool> CheckAuthentication()
    //{
    //    if (_connectivity.NetworkAccess != NetworkAccess.Internet)
    //    {
    //        await Shell.Current.DisplayAlert("No connectivity!",
    //            $"Please check internet and try again.", "OK");
    //        return false;
    //    }

    //    _currentUserService.Clear();

    //    // determine if we are logged in.
    //    var user = _currentUserService.Get();
    //    if (user == null)
    //    {
    //        return false;
    //    }

    //    // check that the refresh token works

    //    var response = await _authenticationService.RefreshAsync(user.Token, user.RefreshToken);

    //    return response.Match(
    //        b => true,
    //        e => false);
    //}

}

