using System.Dynamic;
using System.Reflection;
using Common.Utilities;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MyApiContext:DbContext
    {
        public MyApiContext(DbContextOptions<MyApiContext> options):base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.SetSequentialGuidConvention();
            modelBuilder.AddRestrictDeleteBehavior();
            modelBuilder.AddPluralizingTableName();
            modelBuilder.ApplyConfigurationsFromAssembly(entitiesAssembly);


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            _Cleanstring();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _Cleanstring();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _Cleanstring();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            _Cleanstring();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void _Cleanstring()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var item in changedEntities)
            {
                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p=>p.CanRead && p.CanWrite && p.PropertyType==typeof(string));
                foreach (var property in properties)
                {
                    var propertyValue = property.GetValue(item.Entity)?.ToString();
                    if (propertyValue.HasValue())
                    {
                        var newVal = propertyValue.Fa2En().FixPersianChars();
                        property.SetValue(newVal,item.Entity);
                    }
                }
            }
        }
    }
}
