#if IOS
using CoreFoundation;
using CoreImage;
using Foundation;
using UIKit;
using Vision;
#endif

namespace BestUnitPriceApp.ViewModels;

public partial class ScanInvoiceViewModel : BaseViewModel
{

    public ScanInvoiceViewModel()
    {
        // make sure we can access the camera
        var camera = new Microsoft.Maui.ApplicationModel.Permissions.Camera();
        if (camera.CheckStatusAsync().Result != PermissionStatus.Granted)
        {
            
        }
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
#if IOS
                CIImage InputImage;
                UIImage RawImage;
                VNDetectTextRectanglesRequest TextRectanglesRequest;

                // Setup Vision Text
                TextRectanglesRequest = new VNDetectTextRectanglesRequest(HandleRectangles);
                //TextRectanglesRequest.MaximumObservations = 10; // limit on the number of rectangles to look for - can increase "thinking time"

#endif

            }
        }
    }
#if IOS
    void HandleRectangles(VNRequest request, NSError error)
    {

        var observations = request.GetResults<VNRectangleObservation>();
        if (observations == null)
        {
            //ShowAlert("Processing Error", "Unexpected result type from VNDetectRectanglesRequest.");
            return;
        }
        if (observations.Length < 1)
        {
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                // ClassificationLabel.Text = "No rectangles detected.";
            });
            return;
        }

        Console.WriteLine($"Found {observations.Length} text areas");
    }
#endif
}