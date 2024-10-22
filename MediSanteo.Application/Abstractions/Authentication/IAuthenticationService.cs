using MediSanteo.Domain.Users;

namespace MediSanteo.Application.Abstractions.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken);
    }
}
