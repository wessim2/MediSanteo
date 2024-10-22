using MediSanteo.Application.Abstractions.Authentication;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Users;

namespace MediSanteo.Application.Users.LoginUser
{
    internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AccessTokenResponse>
    {
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            LoginUserCommand request, 
            CancellationToken cancellationToken)
        {
            var result = await _jwtService.GetAccessTokenAsync(
                request.Email,
                request.Password,
                cancellationToken
                );
            if (result.IsFailure)
            {
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
            }

            return new AccessTokenResponse(result.Value);
        }
    }
}
