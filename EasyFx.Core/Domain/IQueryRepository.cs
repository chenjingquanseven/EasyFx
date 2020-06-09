using EasyFx.Core.DependencyInjection;
using EasyFx.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyFx.Core.Domain
{
    /// <summary>
    /// 专注查询操作，AsNoTracking
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IQueryRepository<TEntity>:IScope where TEntity : class, IKey
    {
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate,string key=null);
        Task<TEntity> QueryOneAsync(string id);

        Task<TEntity> QueryOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> predicate,string order=null,bool orderAsc=false);

        Task<(int,List<TEntity>)> QueryPageListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex,int pageSize,string order,bool orderAsc);
    }
}
