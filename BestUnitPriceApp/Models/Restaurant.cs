namespace BestUnitPriceApp.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AddressLine1 { get; set; }
    public object AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public bool GeoCached { get; set; }
}