using System;
namespace BestUnitPriceApp.Models;

public class InvoiceScanResult
{
    public string ItemNumber { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}


public class TextScanResult
{
    public string Text { get; set; }

    public PointF TopLeft { get; set; }

    public PointF TopRight { get; set; }

    public PointF BottomRight { get; set; }

    public PointF BottomLeft { get; set; }
}
