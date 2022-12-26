namespace BestUnitPriceApp.Models;

public class ApplicationUser
{
    public string UserId { get; set; }
    
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }

    public long? SelectedRestaurantId { get; set; }
}