using BestUnitPriceApp.Common.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace BestUnitPriceApp.ViewModels;

public partial class RestaurantsViewModel : BaseViewModel
{
    private readonly IRestaurantService _restaurantService;
    private readonly IConnectivity _connectivity;
    private readonly ICurrentRestaurantService _currentRestaurantService;

    [ObservableProperty]
    bool _isRefreshing;

    [ObservableProperty]
    ObservableCollection<Restaurant> _items;

    [ObservableProperty]
    private Restaurant _selectedRestaurant;
    
    public RestaurantsViewModel(
        IRestaurantService restaurantService,
        ICurrentUserService currentUserService,
        ICurrentRestaurantService currentRestaurantService,
        IConnectivity connectivity)
    {
        _restaurantService = restaurantService;
        _connectivity = connectivity;
        _currentRestaurantService = currentRestaurantService;

        Title = "Restaurants";
        
        SelectedRestaurant = _currentRestaurantService.CurrentRestaurant;    
    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;

        try
        {
            await LoadDataAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task LoadMore()
    {
        //var items = await _restaurantService.GetAsync();

        //foreach (var item in items)
        //{
        //    Items.Add(item);
        //}
    }

    public async Task LoadDataAsync()
    {
        Items = new ObservableCollection<Restaurant>(await _restaurantService.GetAsync());
    }

    [RelayCommand]
    private async void GoToDetails(Restaurant restaurant)
    {
        await Shell.Current.GoToAsync(nameof(RestaurantsDetailPage), true, new Dictionary<string, object>
        {
            { "Restaurant", restaurant }
        });
    }
    
    [RelayCommand]
    public async Task SelectRestaurant(Restaurant restaurant)
    {
        if (_currentRestaurantService.CurrentRestaurant?.Id != restaurant.Id)
        {
            //_selectedRestaurantTracker.TrackRestaurant(restaurant);
            WeakReferenceMessenger.Default.Send(new SelectedRestaurantChangedMessage(restaurant));
        }
    }
}
