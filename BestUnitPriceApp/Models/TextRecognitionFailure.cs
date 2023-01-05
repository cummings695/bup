namespace BestUnitPriceApp.Models;

public class TextRecognitionFailure
{
    public string? OrderCodeColumnName { get; set; }
    public string? PriceColumnName { get; set; }
    public string? QuantityColumnName { get; set; }
    
    public Dictionary<string, List<string>> Columns { get; set; }
}