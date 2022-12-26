namespace BestUnitPriceApp.Models;

public class Order 
{
    public long Id { get; set; }

    //public DateTime CreatedDate { get; set; }
    public decimal? TotalPrice { get; set; }

    public decimal? EstimatedPrice { get; set; }

    public List<OrderItem> Items { get; set; }

    public long VendorId { get; set; }
    public Vendor Vendor { get; set; }

    //public bool? Active { get; set; }

    public long StatusId { get; set; }
    public OrderStatus Status { get; set; }

    public long BatchId { get; set; }
    //public Batch Batch { get; set; }

    public string Invoice { get; set; }

    //public DateTime DeliveryDate { get; set; }

    public DateTime OrderDate { get; set; }

    public string Number { get; set; }

}