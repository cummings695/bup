namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class RestaurantsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    SampleItem item;
}
