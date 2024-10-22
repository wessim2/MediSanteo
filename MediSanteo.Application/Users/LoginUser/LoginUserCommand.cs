﻿using MediSanteo.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Users.LoginUser
{
    public sealed record LoginUserCommand(string Email,string Password) : ICommand<AccessTokenResponse>;
}
