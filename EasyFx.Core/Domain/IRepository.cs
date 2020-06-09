using EasyFx.Core.DependencyInjection;
using EasyFx.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyFx.Core.Domain
{
    /// <summary>
    /// 一般操作仓储，查询用追踪
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>: IScope where T : class, IAggregateRoot
    {
        Task<T> InsertAsync(T entity);

        Task<List<T>> InsertRangeAsync(List<T> entities);

        T Update(T entity);

        List<T> UpdateRange(List<T> entities);

        T Delete(T entity);

        List<T> DeleteRange(List<T> entities);

        Task<T> GetAsync(string id);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    }
}
