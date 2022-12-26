namespace BestUnitPriceApp.Models;

public class ProductPrice
{
    public long Id { get; set; }

    public decimal? Comparison { get; set; }

    public long VendorId { get; set; }

    public Vendor Vendor { get; set; }

    public long ProductInfoId { get; set; }

    public ProductInfo ProductInfo { get; set; }

    public bool Deleted { get; set; }

    public string ToBestUnitPrice
    {
        get
        {
            if (Comparison == null) return "BUP";

            return Comparison.Value.ToString("N2");
        }
    }
}
