using MediatR;

namespace EasyFx.Core.Bus.Events
{
    public abstract class EventMessage:Message,INotification
    {
        
    }
}