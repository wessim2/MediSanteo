using MediSanteo.Application.Abstractions.Messaging;

namespace MediSanteo.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Role
        ) : ICommand<Guid> ;
}
