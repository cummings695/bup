namespace BestUnitPriceApp.Models;

public class AuthorizationTicket
{
    public string AccessToken { get; set; }
    
    public DateTimeOffset AccessTokenExpiration { get; set; }
    
    public string RefreshToken { get; set; }
}