using MediatR;

namespace EasyFx.Core.Bus.Events
{
    public interface IEventMessageHandler<TEvent> : INotificationHandler<TEvent> where TEvent : EventMessage
    {
    }
}