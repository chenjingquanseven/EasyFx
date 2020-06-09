using EasyFx.Core.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyFx.Core.Domain
{
    public interface IUnitOfWork : IDisposable, IScope
    {
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IUnitOfWork> BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        Task CommitTransactionAsync();
    }
}
