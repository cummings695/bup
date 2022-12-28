namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(InventoryItem), "Item")]
public partial class ItemsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    InventoryItem item;

    [ObservableProperty]
    private ObservableCollection<ProductPrice> _productPrices;
}
