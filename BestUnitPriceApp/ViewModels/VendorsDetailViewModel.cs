namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Vendor), "Vendor")]
public partial class VendorsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    Vendor _vendor;
}
