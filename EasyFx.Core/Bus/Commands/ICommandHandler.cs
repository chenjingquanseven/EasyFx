using MediatR;

namespace EasyFx.Core.Bus.Commands
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
        where TCommand : CommandMessage
    {
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : CommandMessage<TResponse>
    {

    }
}