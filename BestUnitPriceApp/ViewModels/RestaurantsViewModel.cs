namespace BestUnitPriceApp.ViewModels;

public partial class RestaurantsViewModel : BaseViewModel
{
    private readonly IRestaurantService _restaurantService;
    private readonly SelectedRestaurantTracker _selectedRestaurantTracker;
    private readonly IConnectivity _connectivity;
    private readonly ICurrentRestaurantService _currentRestaurantService;
    private readonly RestaurantReporter _reporter; 

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    ObservableCollection<Restaurant> items;

    [ObservableProperty]
    private Restaurant _selectedRestaurant;
    
    public RestaurantsViewModel(
        IRestaurantService restaurantService,
        ICurrentUserService currentUserService,
        SelectedRestaurantTracker selectedRestaurantTracker,
        ICurrentRestaurantService currentRestaurantService,
        IConnectivity connectivity)
    {
        _selectedRestaurantTracker = selectedRestaurantTracker;
        _restaurantService = restaurantService;
        _connectivity = connectivity;
        _currentRestaurantService = currentRestaurantService;
        _reporter = new RestaurantReporter("Default");
        _reporter.Subscribe(_selectedRestaurantTracker);

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
        var items = await _restaurantService.GetAsync();

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    public async Task LoadDataAsync()
    {
        Items = new ObservableCollection<Restaurant>(await _restaurantService.GetAsync());
    }

    [RelayCommand]
    private async void GoToDetails(Restaurant item)
    {
        await Shell.Current.GoToAsync(nameof(RestaurantsDetailPage), true, new Dictionary<string, object>
        {
            { "Item", item }
        });
    }
}
