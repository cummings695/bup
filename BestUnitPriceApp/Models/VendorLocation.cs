namespace BestUnitPriceApp.Models;

public class VendorLocation
{
    public long Id { get; set; }
    public string Name { get; set; }

    public string ContactName { get; set; }

    public string Website { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int? DeliveryRange { get; set; }

    public bool? Active { get; set; }

    public bool? IsGeoCoded { get; set; }

}