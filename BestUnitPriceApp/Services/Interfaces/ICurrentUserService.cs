
namespace BestUnitPriceApp.Services;

public interface ICurrentUserService
{
    ApplicationUser Set(string token, string refresh);
        
    ApplicationUser Set(AuthorizationTicket ticket);
        
    ApplicationUser Get();

    void Store();
    void Clear();
}