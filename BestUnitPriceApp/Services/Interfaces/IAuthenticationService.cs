using LanguageExt.Common;

namespace BestUnitPriceApp.Services;

public interface IAuthenticationService
{
    AuthorizationTicket Login(string email, string password);

    Task<AuthorizationTicket> LoginAsync(string email, string password);

    Task<Result<AuthorizationTicket>> RefreshAsync(string accessToken, string refreshToken);
}