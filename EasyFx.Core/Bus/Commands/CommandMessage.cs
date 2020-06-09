using MediatR;

namespace EasyFx.Core.Bus.Commands
{
    public abstract class CommandMessage : Message
    {
    }

    /// <summary>
    /// 命令
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class CommandMessage<TResponse> : Message, IRequest<TResponse>
    {
    }
}