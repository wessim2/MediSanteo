
using MediSanteo.Application.Abstractions.Messaging;

namespace MediSanteo.Application.Users.GetLoggedInUser
{
    public sealed record GetLoggedInUserQuery() : IQuery<UserResponse>;
}
