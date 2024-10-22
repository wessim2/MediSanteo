using MediSanteo.Application.Abstractions.Authentication;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                new Domain.Shared.Email(request.Email));

            var identityId = await _authenticationService.RegisterAsync(
                user, 
                request.Password,
                cancellationToken); 

            user.SetIdentityId(identityId);

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
