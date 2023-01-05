namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Order), "Order")]
public partial class OrdersDetailViewModel : BaseViewModel
{
    [ObservableProperty] Order _order;

    public OrdersDetailViewModel()
    {
        this.Title = Order?.Vendor?.Name;
    }

    [RelayCommand]
    public async Task ScanInvoice()
    {
        await Shell.Current.GoToAsync($"/{nameof(ScanInvoicePage)}");
    }
}