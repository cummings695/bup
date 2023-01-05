using System;
using LanguageExt.Common;

namespace BestUnitPriceApp.Services.Interfaces;

public interface IInvoiceScannerService
{
    public Result<List<InvoiceScanResult>> ScanInvoice(string localFilePath);
}

