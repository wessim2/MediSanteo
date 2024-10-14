using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendAsync(MediSanteo.Domain.Shared.Email email, string title, string body);
    }
}
