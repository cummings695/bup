namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Batch), "Batch")]
public partial class BatchesDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    Batch _batch;

    [RelayCommand]
    private async void GoToDetails(Order order)
    {
        await Shell.Current.GoToAsync(nameof(OrdersDetailPage), true, new Dictionary<string, object>
        {
            { "Order", order }
        });
    }

    [RelayCommand]
    private async void ScanOrder(Order order)
    {
        await Shell.Current.GoToAsync($"/{nameof(ScanInvoicePage)}");
    }
}