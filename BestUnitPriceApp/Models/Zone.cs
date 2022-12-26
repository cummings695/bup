namespace BestUnitPriceApp.Models;

public class Zone
{
    public long Id { get; set; }

    public string Name { get; set; }

    //public string Letter { get; set; }

    public int SortOrder { get; set; }
    public List<ShelvingUnit> ShelvingUnits { get; set; }
}