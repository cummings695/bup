namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Restaurant), "Restaurant")]
public partial class RestaurantsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    Restaurant _restaurant;
}
