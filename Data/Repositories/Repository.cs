using Common.Utilities;
using Data.Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Diagnostics.Metrics;
using System.Threading;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {

        private readonly MyApiContext _context;
          
        public DbSet<TEntity> Entities { get; set; }
        public virtual IQueryable<TEntity> Table =>Entities;

        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking().AsQueryable();

        public Repository(MyApiContext conext)
        {
           _context=conext;
            Entities=_context.Set<TEntity>();
        }
        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull<TEntity>(entity,nameof(entity));
            Entities.Add(entity);
            if(saveNow)
                _context.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull<TEntity>(entity,nameof(entity));
            await Entities.AddAsync(entity,cancellationToken);
            if(saveNow)
               await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.AddRange(entities);
            if(saveNow)
                _context.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            await Entities.AddRangeAsync(entities,cancellationToken);
            if(saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }

        public void Delete(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if(saveNow)
                _context.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if(saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Entities.RemoveRange(entities);
            if(saveNow)
                 _context.SaveChanges();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Entities.RemoveRange(entities);
            if(saveNow)
                 await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            var entity= await Entities.FindAsync(ids);
            Assert.NotNull<TEntity>(entity,nameof(entity));
            return entity;
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(_context, nameof(entity));
            Entities.Update(entity);
            if(saveNow)
                _context.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(_context, nameof(entity));
            Entities.Update(entity);
            if(saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }

        public void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
             Entities.UpdateRange(entities);
            if(saveNow)
                _context.SaveChanges();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Entities.UpdateRange(entities);
            if(saveNow)
                await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
