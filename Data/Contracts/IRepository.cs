using Entities;

namespace Data.Contracts;

public interface IRepository<TEntity> where TEntity : class,IEntity
{

    #region Async Methods

    Task AddAsync(TEntity entity,CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(CancellationToken cancellationToken,params object[] ids);
    Task UpdateAsync(TEntity entity,CancellationToken cancellationToken);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity,CancellationToken cancellationToken);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken);

    #endregion

    #region Sync Methods

    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    TEntity GetById(params object[] ids);

    #endregion
}