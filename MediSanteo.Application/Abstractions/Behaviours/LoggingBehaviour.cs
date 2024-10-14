using MediatR;
using Microsoft.Extensions.Logging;
using MediSanteo.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Abstractions.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommmand
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                _logger.LogInformation("Executing cammand {Command}", name);

                var result = await next();

                _logger.LogInformation("{Command} processed successfully", name);

                return result;

            }catch(Exception ex)
            {
                _logger.LogError(ex, "Command {Command} processing failed", name);

                throw;
            }
        }
    }
}
