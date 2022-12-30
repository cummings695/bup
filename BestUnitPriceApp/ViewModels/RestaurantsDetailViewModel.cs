using BestUnitPriceApp.Common.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Restaurant), "Restaurant")]
public partial class RestaurantsDetailViewModel : BaseViewModel
{
    private readonly ICurrentRestaurantService _currentRestaurantService;
    
    [ObservableProperty]
    Restaurant _restaurant;

    public RestaurantsDetailViewModel(ICurrentRestaurantService currentRestaurantService)
    {
        _currentRestaurantService = currentRestaurantService;
    }
    
    [RelayCommand]
    public async Task SelectRestaurant(Restaurant restaurant)
    {
        if (_currentRestaurantService.CurrentRestaurant?.Id != restaurant.Id)
        {
            WeakReferenceMessenger.Default.Send(new SelectedRestaurantChangedMessage(restaurant));
        }

        if (_currentRestaurantService.CurrentRestaurant == null)
        {
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }
    }
}
