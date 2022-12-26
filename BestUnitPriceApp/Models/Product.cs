namespace BestUnitPriceApp.Models;

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Notes { get; set; }

    public List<ProductPrice> Prices { get; set; }

    public long DefaultUnitOfMeasureId { get; set; }

    public Unit DefaultUnitOfMeasure { get; set; }

    public float? DefaultQuantity { get; set; }

    public float? DefaultCount { get; set; }
}
