using System;
using MediatR;

namespace EasyFx.Core.Bus
{
    public abstract class Message:IRequest
    {
        protected Message()
        {
            this.Id = Guid.NewGuid();
            this.CreationTime = DateTime.UtcNow;
            this.MessageType = GetType().FullName;
        }
        /// <summary>
        /// 唯一Id
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; }
    }
}