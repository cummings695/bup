namespace BestUnitPriceApp.ViewModels;

[QueryProperty(nameof(Order), "Order")]
public partial class OrdersDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    Order _order;

    public OrdersDetailViewModel()
    {
        this.Title = Order?.Vendor?.Name;
    }
    
    [RelayCommand]
    public async Task ScanInvoice()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);

                //await Shell.Current.GoToAsync($"//{nameof(ScanInvoicePage)}");
            }
        }
    }
}