using MediatR;
using MediSanteo.Domain.Abstractions;


namespace MediSanteo.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result> , IBaseCommmand
    {
    }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>> , IBaseCommmand
    {
    }
    public interface IBaseCommmand { }
}
