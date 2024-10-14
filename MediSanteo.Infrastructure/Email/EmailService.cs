using MediSanteo.Application.Abstractions.Email;


namespace MediSanteo.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync(Domain.Shared.Email email, string title, string body)
        {
            return Task.CompletedTask;
        }
    }
}
