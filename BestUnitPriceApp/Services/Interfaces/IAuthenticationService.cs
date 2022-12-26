using BestUnitPrice.Application.Common.Models;

namespace BestUnitPriceApp.Services;

public interface IAuthenticationService
{
    AuthorizationTicket Login(string email, string password);

    Task<AuthorizationTicket> LoginAsync(string email, string password);

    Task<(Result Result, AuthorizationTicket Ticket)> RefreshAsync(string accessToken, string refreshToken);
}