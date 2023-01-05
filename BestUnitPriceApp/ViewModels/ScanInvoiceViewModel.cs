using BestUnitPriceApp.Services.Interfaces;

namespace BestUnitPriceApp.ViewModels;

public partial class ScanInvoiceViewModel : BaseViewModel
{
    readonly IInvoiceScannerService _invoiceScannerService;
    public ScanInvoiceViewModel(IInvoiceScannerService invoiceScannerService)
    {
        // make sure we can access the camera
        // var camera = new Microsoft.Maui.ApplicationModel.Permissions.Camera();
        // if (camera.CheckStatusAsync().Result != PermissionStatus.Granted)
        // {
        // }
        _invoiceScannerService = invoiceScannerService;
    }

    [ObservableProperty]
    public ImageSource _scannedImage;

    [RelayCommand]
    public async Task ScanInvoice()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            var actions = new string[] { "Camera", "Photos" };

            string action = await Shell.Current.DisplayActionSheet(
                "Capture Image From?", "Cancel", null, actions);
            FileResult photo = null;
            if (action == actions[0])
            {
                photo = await MediaPicker.Default.CapturePhotoAsync();
            }

            if (action == actions[1])
            {
                photo = await MediaPicker.Default.PickPhotoAsync();
            }

            if (photo == null) return;

            // save the file into local storage
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using var stream = await photo.OpenReadAsync();
            using var newImage = File.OpenWrite(newFile);
            await stream.CopyToAsync(newImage);
            ScannedImage = ImageSource.FromFile(newFile);

            var results = _invoiceScannerService.ScanInvoice(newFile);
//#if IOS
            //UIImage uiImage = UIImage.FromFile(localFilePath);
            //CIImage ciImage = new CIImage(uiImage);
            
            //VNDetectTextRectanglesRequest textRectangleRequest;

            // Setup Vision Text
            //textRectangleRequest = new VNDetectTextRectanglesRequest(HandleRectangles);



            //var handler = new VNImageRequestHandler(ciImage, UIImageOrientation.Up, new VNImageOptions());
            //
            // DispatchQueue.DefaultGlobalQueue.DispatchAsync(()=>{
            //     handler.Perform(new VNRequest[] {TextRectangleRequest}, out NSError error);
            // });
            //var results = TextRectangleRequest.GetResults<VNObservation>(); // limit on the number of rectangles to look for - can increase "thinking time"
            // foreach (var observation in results)
            // {
            //     Console.WriteLine(observation.ToString());
            // }
//#endif
        }
    }

//#if IOS

//#endif
}