﻿using MediatR;
using MediSanteo.Application.Users.GetLoggedInUser;
using MediSanteo.Application.Users.LoginUser;
using MediSanteo.Application.Users.RegisterUser;
using MediSanteo.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediSanteo.Controllers.Users
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        [HasPermission(Permissions.UsersRead)]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            var result = await _sender.Send(query,cancellationToken);

            return Ok(result.Value);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken
            ) 
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.Role
                );        

            var result = await _sender.Send(command,cancellationToken);

            if ( result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);  
        
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginUserRequest request,
            CancellationToken cancellationToken
            ) 
        {
            var command = new LoginUserCommand(
                request.Email,
                request.Password
                );
        
            var result = await _sender.Send(command,cancellationToken);

            if ( result.IsFailure)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
