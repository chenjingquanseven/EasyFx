using EasyFx.Core.Bus.Commands;
using EasyFx.Core.Bus.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFx.Core.Bus
{
    public class MediatorBus:IMessageBus
    {
        private readonly IMediator _mediator;

        public MediatorBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task SendAsync(CommandMessage commandMessage, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mediator.Send(commandMessage,cancellationToken);
        }

        public  Task<TResponse> SendAsync<TResponse>(CommandMessage<TResponse> commandMessage, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _mediator.Send<TResponse>(commandMessage,cancellationToken);
        }

        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) where TEvent : EventMessage
        {
            return _mediator.Publish(@event);
        }
    }
}