namespace BestUnitPriceApp.Models;

public class Vendor 
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName
    {
        get
        {
            return string.IsNullOrEmpty(Name) ? "NO VENDOR" : Name;
        }
    }
    public string ContactName { get; set; }

    //public string Number { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public string Email { get; set; }

    public string Phone1 { get; set; }

    public string Phone2 { get; set; }

    public string Fax { get; set; }

    public string CellPhone { get; set; }

    public string Notes { get; set; }

    public float? MinimumOrder { get; set; }

    public string AccountNumber { get; set; }

    public float? OrderTotal { get; set; }

    public float? IncentivePercentage { get; set; }

    public string Color { get; set; }

    public int? SortOrder { get; set; }

    public bool Active { get; set; }

    public string Website { get; set; }

    public string ContactEmail { get; set; }

    public bool GeoCached { get; set; }

    public int DeliveryRange { get; set; }

    public List<VendorLocation> Locations { get; set; }

}