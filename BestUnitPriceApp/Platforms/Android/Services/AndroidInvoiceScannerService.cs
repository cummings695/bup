using System;
using BestUnitPriceApp.Services.Interfaces;
using LanguageExt.Common;

namespace BestUnitPriceApp.Platforms.Android.Services;

public class AndroidInvoiceScannerService : IInvoiceScannerService
{
    public Result<List<InvoiceScanResult>> ScanInvoice(string localFilePath)
    {
        return new List<InvoiceScanResult>();
    }
}