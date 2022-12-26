namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ItemsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    SampleItem item;
}
