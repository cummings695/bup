namespace BestUnitPriceApp.Models;

public class ProductInfo
{
    public long Id { get; set; }

    public string OrderCode { get; set; }

    public string Brand { get; set; }

    public string Description { get; set; }

    public string VendorDescription { get; set; }

    public float? Quantity { get; set; }

    /// <summary>
    /// this is really measure.
    /// </summary>
    public float? Count { get; set; }

    public long UnitOfMeasureId { get; set; }

    public Unit UnitOfMeasure { get; set; }

    public bool? Deselected { get; set; }

    public float Price { get; set; }

    public float UnitPrice { get; set; }

    public bool? IsPreferred { get; set; }

    public string ManufacturerCode { get; set; }

    public DateTime UpdatedDate { get; set; }
}
