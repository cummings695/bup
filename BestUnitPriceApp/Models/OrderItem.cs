namespace BestUnitPriceApp.Models;

public class OrderItem
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public float UnitPrice { get; set; }

    public int? Quantity { get; set; }

    public int? Received { get; set; }

    public float ItemPrice { get; set; }

    public float? ReceivedPrice { get; set; }
    public float? ReceivedUnitPrice { get; set; }

    public string OrderCode { get; set; }

    public float TotalPrice { get; set; }

    public float ReceivedTotal { get; set; }

    public long ProductId { get; set; }
    //public Product Product { get; set; }

    public long InventoryItemId { get; set; }

    public int? CurrentInventory { get; set; }

    public long OrderId { get; set; }
    //public OrderViewModel Order { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public IList<Vendor> PreviousVendors { get; set; }

    public decimal LostSavings { get; set; }

}