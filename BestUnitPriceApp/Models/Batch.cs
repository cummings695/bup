namespace BestUnitPriceApp.Models;

public class Batch
{
    public long Id { get; set; }

    public string Number { get; set; }

    public List<Order> Orders { get; set; }

    public Restaurant Owner { get; set; }

    public BatchStatus Status { get; set; }

    //new features for deactivating and activating a vendor
    public List<Vendor> DeactivatedVendors { get; set; }

    //public OrderViewModel GhostOrder { get; set; }

    public CostSavings CostSavings { get; set; }

    public bool Active { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime CreateDate { get; set; }
}