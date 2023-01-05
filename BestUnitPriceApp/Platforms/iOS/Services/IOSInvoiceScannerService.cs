using System;
using BestUnitPriceApp.Common.Messages;
using BestUnitPriceApp.Services.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using CoreFoundation;
using CoreImage;
using Foundation;
using LanguageExt.Common;
using LanguageExt.Pipes;
using UIKit;
using Vision;

namespace BestUnitPriceApp.Platforms.IOS.Services;

public class IOSInvoiceScannerService : IInvoiceScannerService
{
    public TextScanResult[] TextScanResults;

    public Result<List<InvoiceScanResult>> ScanInvoice(string localFilePath)
    {
        UIImage uiImage = UIImage.FromFile(localFilePath);
        CIImage ciImage = new CIImage(uiImage);

        // Setup Vision Text
        VNDetectTextRectanglesRequest textRectangleRequest = new VNDetectTextRectanglesRequest(HandleRectangles);

        var handler = new VNImageRequestHandler(ciImage, ImageIO.CGImagePropertyOrientation.Up, new VNImageOptions());

        DispatchQueue.DefaultGlobalQueue.DispatchAsync(() => {
            if (handler.Perform(new VNRequest[] {textRectangleRequest}, out NSError error))
            {
                Console.WriteLine($"Found {TextScanResults.Length} results");
            }

            var columns = DetermineColumnarData(TextScanResults);
            string? orderCodeColumnName = null;
            string? priceColumnName = null;
            string? quantityColumnName = null;
            
            foreach (string columnHeader in columns.Keys)
            {
                switch (columnHeader.ToLower())
                {
                    case "ordercode" :
                        orderCodeColumnName = columnHeader;
                        break;
                    case "price" :
                        priceColumnName = columnHeader;
                        break;
                    case "quantity":
                        quantityColumnName = columnHeader;
                        break;
                    default:
                        break;
                }
            }

            if (orderCodeColumnName == null || priceColumnName == null || quantityColumnName == null)
            {
                var result = new TextRecognitionFailure
                {
                    OrderCodeColumnName = orderCodeColumnName,
                    PriceColumnName = priceColumnName,
                    QuantityColumnName = quantityColumnName,
                    Columns = columns
                };
                WeakReferenceMessenger.Default.Send(new TextRecognitionFailureMessage(result));
            }
        });

        return new List<InvoiceScanResult>();
    }

    void HandleRectangles(VNRequest request, NSError error)
    {
        var observations = request.GetResults<VNTextObservation>();
        if (observations == null)
        {
            //ShowAlert("Processing Error", );
            return;
            //return new Result<List<TextScanResult>>( new ApplicationException("Unexpected result type from VNDetectRectanglesRequest."));
        }

        if (observations.Length < 1)
        {
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                // ClassificationLabel.Text = "No rectangles detected.";
            });
            return; // new Result<List<TextScanResult>>(new ApplicationException("No Text Detected."));
        }

        TextScanResults = observations.Select(r => new TextScanResult
        {
            TopLeft = new PointF ( ((float)r.TopLeft.X), ((float)r.TopLeft.Y))
        }).ToArray();

        Console.WriteLine($"Found {observations.Length} text areas");
    }

    private Dictionary<string, List<string>> DetermineColumnarData(TextScanResult[] TextScanResults)
    {
        return new Dictionary<string, List<string>>();
    }
    
    /*
    public class Rectangle
    {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
    }
    
    public class ColumnDetector
    {
        public static int NumColumns(List<Rectangle> rectangles, int numClusters)
        {
            // Check if the collection is empty
            if (rectangles.Count == 0)
            {
                return 0;
            }

            // Create a ML.NET context
            var context = new MLContext();

            // Create a data view from the rectangles
            var data = context.Data.LoadFromEnumerable(rectangles);

            // Define the feature columns (x1 and slope)
            var pipeline = context.Transforms.Conversion.MapValueToKey("Label", "x1")
                .Append(context.Transforms.CustomMapping(
                    (inputs, output) =>
                    {
                        // Calculate the slope of the rectangle
                        float slope = (inputs.y2 - inputs.y1) / (float)(inputs.x2 - inputs.x1);
                        output.Slope = slope;
                    },
                    null))
                .Append(context.Clustering.Trainers.KMeans("Features", numClusters));

            // Train the model
            var model = pipeline.Fit(data);

            // Get the cluster assignments for the rectangles
            var assignments = model.Transform(data).AsEnumerable<Prediction>(context, reuseRowObject: true);

            // Count the number of unique clusters
            return assignments.Select(p => p.PredictedLabel).Distinct().Count();
        }
    }
    */
}

