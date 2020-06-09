using EasyFx.Core.Bus.Commands;
using EasyFx.Core.Bus.Events;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFx.Core.Bus
{
    public interface IMessageBus
    {
        Task SendAsync(CommandMessage commandMessage,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TResponse> SendAsync<TResponse>(CommandMessage<TResponse> commandMessage,
            CancellationToken cancellationToken = default(CancellationToken));

        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default(CancellationToken)) where TEvent : EventMessage;
    }
}