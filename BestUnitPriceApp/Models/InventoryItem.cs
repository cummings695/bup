using System.ComponentModel.DataAnnotations;

namespace BestUnitPriceApp.Models;

public class InventoryItem
{
    public long Id { get; set; }
    
    public Product Product { get; set; }

    public int? BaseQuantity { get; set; }

    public int? CurrentQuantity { get; set; }

    public int? OrderAmount { get; set; }

    public Zone Zone { get; set; }

    public ShelvingUnit ShelvingUnit { get; set; }

    public int? ShelfNumber { get; set; }

    public string ShelvingLocation => $"{ShelvingUnit?.UnitNumber}-{ShelfNumber}";

    public bool? Active { get; set; }

    public long OwnerId { get; set; }

    public long? ZoneId { get; set; }

    public long? ShelvingUnitId { get; set; }
}
