using System.Linq.Expressions;
using Entities;

namespace Data.Contracts;

public interface IRepository<TEntity> where TEntity : class,IEntity
{

    #region Async Methods

    Task AddAsync(TEntity entity,CancellationToken cancellationToken,bool saveNow=true);
    Task AddRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken,bool saveNow=true);
    Task<TEntity> GetByIdAsync(CancellationToken cancellationToken,params object[] ids);
    Task UpdateAsync(TEntity entity,CancellationToken cancellationToken,bool saveNow=true);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken,bool saveNow=true);
    Task DeleteAsync(TEntity entity,CancellationToken cancellationToken,bool saveNow=true);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken,bool saveNow=true);

    Task<bool> IsExistAsync(Expression<Func<TEntity,bool>> expression, CancellationToken cancellationToken);

    #endregion

    #region Sync Methods

    void Add(TEntity entity,bool saveNow=true);
    void AddRange(IEnumerable<TEntity> entities,bool saveNow=true);
    void Update(TEntity entity,bool saveNow=true);
    void UpdateRange(IEnumerable<TEntity> entities,bool saveNow=true);
    void Delete(TEntity entity, bool saveNow = true);
    void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
    TEntity GetById(params object[] ids);

    #endregion
}