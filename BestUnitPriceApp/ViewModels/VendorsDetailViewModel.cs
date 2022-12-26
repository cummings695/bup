namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class VendorsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    SampleItem item;
}
