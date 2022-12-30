namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(InventoryItem), "InventoryItem")]
public partial class ItemsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    InventoryItem _inventoryItem;

    [ObservableProperty]
    private ObservableCollection<ProductPrice> _productPrices;

    public ItemsDetailViewModel()
    {
        Console.WriteLine("");
    }
}
